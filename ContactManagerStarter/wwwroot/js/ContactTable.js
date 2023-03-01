"use strict";

$(function () {
    loadContactTable();
    initSignalr();

    $(document).on("dblclick", ".editContact", function () {
        let buttonClicked = $(this);
        let id = buttonClicked.data("id");
        $.ajax({
            type: "GET",
            url: "/Contacts/EditContact",
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                $('#EditContactModalContent').html(data);
                $('#modal-editContact').modal('show');
                $("#ServerErrorAlert").hide();
            },
            error: function () {
                $("#ServerErrorAlert").show();
            }
        });
    });

    $(document).on("click", ".deleteContact", function () {
        let buttonClicked = $(this);
        let id = buttonClicked.data("id");
        $("#deleteContactConfirmed").data("id", id);
    });

    $(document).on("click", "#addNewEmail", function () {
        let emailAddress = $('#newEmailAddress').val();
        let emailAddressType = $('#newEmailAddressType').val();
        let emailTypeClass;
        let noPrimaryCheckbox = $(".primaryCheckbox").length === 0;

        if (emailAddressType === "Personal") {
            emailTypeClass = "badge-primary"; //blue badge
        } else {
            emailTypeClass = "badge-success"; //green badge
        }

        if (validateEmail(emailAddress)) {
            $("#emailList").append(
            '<li class="list-group-item emailListItem" data-email="' + emailAddress + '" data-type="' + emailAddressType + '"data-primary="' + noPrimaryCheckbox +'">' +
            '<span class="badge ' + emailTypeClass + ' m-l-10">' + emailAddressType + '</span>' +
            '<span class="m-l-10" > <input type="checkbox" class="primaryCheckbox" onclick="setPrimary(this)" @(emailAddressIsPrimary || noPrimaryCheckbox ? "checked" : "")> Primary</span> ' +
            '<span class="m-l-20">' + emailAddress + ' </span>' +
            '<a class="redText pointer float-right removeEmail" title="Delete Email">X</a>' +
            '</li>');
            $('#newEmailAddress').val("");  
            $('#newEmailAddress').removeClass("invalidInput");
            $('#invalidEmailFeedback').hide();
            if ($(".primaryCheckbox").length === 1) {
                $(".primaryCheckbox").prop("checked", true);
            }
        } else {
            $('#newEmailAddress').addClass("invalidInput");
            $('#invalidEmailFeedback').show();
        }
    });

    $(document).on("click", "#addNewAddress", function () {
        let street1 = $('#newAddressStreet1').val();
        let street2 = $('#newAddressStreet2').val();
        let city = $('#newAddressCity').val();
        let state = $('#newAddressState').val();
        let zip = $('#newAddressZip').val()

        let address = street1 + " " +
            street2 + " " +
            city + " " +
            state + " " +
            zip;

        let addressType = $('#newAddressType').val();
        let addressTypeClass;

        if (addressType === "Primary") {
            addressTypeClass = "badge-primary"; //blue badge
        } else {
            addressTypeClass = "badge-success"; //green badge
        }

        //if (validateAddress(address)) {
            $("#addressList").append(
                '<li class="list-group-item addressListItem" data-street1="' + street1 + '" data-street2="' + street2 + '" data-city="' +
                city + '" data-state="' + state + '" data-zip="' + zip + '" data-type="' + addressType + '">' +
                '<span class="badge ' + addressTypeClass + ' m-l-10">' + addressType + '</span>' +
                '<span class="m-l-20">' + address + ' </span>' +
                '<a class="redText pointer float-right removeAddress" title="Delete Address">X</a>' +
                '</li>');

            $('#newAddressStreet1').val("");
            $('#newAddressStreet2').val("");
            $('#newAddressCity').val("");
            $('#newAddressState').val("");
            $('#newAddressZip').val("");

            //$('.addressInput').removeClass("invalidInput");

            //$('.addressFeedback').hide();
        //} 
    });

    $(document).on("click", ".removeEmail", function () {
        $(this).parent().remove();
    });

    $(document).on("click", ".removeAddress", function () {
        $(this).parent().remove();
    });

    $(document).on("click", "#saveContactButton", () => {
        function getEmailAddresses() {
            var primaryCheckboxes = $('.primaryCheckbox');
            var emailAddressIsPrimaryIndex = null;

            primaryCheckboxes.each(function (index) {
                if ($(this).is(':checked')) {
                    emailAddressIsPrimaryIndex = index;
                    return false;
                }
            });

            return $(".emailListItem").map(function (index) {
                var isPrimary = (index === emailAddressIsPrimaryIndex);
                return {
                    Email: $(this).data("email"),
                    Type: $(this).data("type"),
                    IsPrimary: isPrimary
                };
            }).get();
        }
  
        function getAddresses() {
            return $(".addressListItem").map(function () {
                return {
                    street1: $(this).data("street1"),
                    street2: $(this).data("street2"),
                    city: $(this).data("city"),
                    state: $(this).data("state"),
                    zip: $(this).data("zip"),
                    Type: $(this).data("type")
                }
            }).get();
        }

        function validateInputs(data) {
            let isValid = true;
            $('.invalidMessage').hide();
            $('.form-control').removeClass("invalidInput");

            if (data.FirstName == "") {
                $('#editContactFirstName').addClass("invalidInput");
                $('#invalidFirstNameFeedback').show();
                isValid = false;
            }
            if (data.LastName == "") {
                $('#editContactLastName').addClass("invalidInput");
                $('#invalidLastNameFeedback').show();
                isValid = false;
            }

            return isValid;
        }

        let data = {
            ContactId: $("#contactId").val() || "00000000-0000-0000-0000-000000000000",
            Title: $("#editContactTitle").val(),
            FirstName: $("#editContactFirstName").val(),
            LastName: $("#editContactLastName").val(),
            DOB: $("#editContactDOB").val(),
            Emails: getEmailAddresses(),
            Addresses: getAddresses()
        };


        if (validateInputs(data)) {
            $.ajax({
                type: "POST",
                url: "/Contacts/SaveContact",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data),
                datatype: "json",
                success: function () {
                    $('#modal-editContact').modal('hide');
                    $("#ServerErrorAlert").hide();
                    //loadContactTable();
                },
                error: function () {
                    $('#modal-editContact').modal('hide');
                    $("#ServerErrorAlert").show();
                }
            });
        }
    });

    $("#newContactButton").click(function () {
        $.ajax({
            type: "GET",
            url: "/Contacts/NewContact",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                $('#EditContactModalContent').html(data);
                $('#modal-editContact').modal('show');
                $("#ServerErrorAlert").hide();
            },
            error: function () {
                $("#ServerErrorAlert").show();
            }
        });
    });

    $("#deleteContactConfirmed").click(function () {
        let id = $("#deleteContactConfirmed").data("id");
        $.ajax({
            type: "DELETE",
            url: "/Contacts/DeleteContact",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                $("#ServerErrorAlert").hide();
                //loadContactTable(); 
            },
            error: function () {
                $("#ServerErrorAlert").show();
            }
        });
    });
 
    function loadContactTable() {
        $.ajax({
            type: "GET",
            url: "/Contacts/GetContacts",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                $('#contactTable').html(data);
                $("#ServerErrorAlert").hide();
                $("#tableHeader").show();
            },
            error: function () {
                $("#ServerErrorAlert").show();
            }
        });
    }

    function validateEmail(email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (email) {
            return regex.test(email);
        } else {
            return false;
        }
    }

    function initSignalr() {
        var connection = new signalR.HubConnectionBuilder().withUrl("/contactHub").build();

        connection.on("Update", function () {
            //console.log("update");
            loadContactTable();
        });

        connection.start();
    }
});

function setPrimary(checkbox) {
    $(".primaryCheckbox").prop("checked", false);
    $(checkbox).prop("checked", true);
}


