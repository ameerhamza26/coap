﻿

function showAlertBasicInfoInd2() {

    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

}


function showallBusiness() {
    $('#BucontactInformation').show();
    $('#BuheadContactInfo').show();
    $('#BubusinessDetail').show();
    $('#BubankRelationship').show();
    $('#BufinanceInformation').show();
    $('#BushareHolderInformation').show();
    $('#BusFatca').show();
}

function showall() {
    // $('#basicInformation').addClass("active");
    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();

}

function ShowAllAccountOpenBusiness() {
    $('#OperatingInstruction').show();
    $('#ContactInformation').show();
    $('#AuthorizedPerson').show();
    $('#WhoAuthorized').show();
    $('#KnowYourCustomer').show();
    $('#CertDepositInfo').show();

    $('#DocumentRequired').show();
}

function ShowAllAccountOpenIndividual() {
    // $('#basicInformation').addClass("active");
    $('#OperatingInstruction').show();
    $('#ApplicantInformation').show();
    $('#AddressInformation').show();
    $('#NextOfKin').show();
    $('#KnowYourCustomer').show();
    $('#CertDepositInfo').show();

    $('#DocumentRequired').show();
}


function getHide() {
    //    $('#otherIdentity').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();

    //   $('#basicInformation').removeClass("active");

}

function BusinessAccountOpenPendingAlert(var1, var2, var3, var4, var5, var6,var7, mesg) {

    if (mesg == "null") {
        $('#alerts').hide();
    }
    else {
        $('#alerts').append(
               '<div class="alert alert-success">' +
                   '<button type="button" class="close" data-dismiss="alert">' +
                   '&times;</button>' + mesg + '</div>');

    }


    document.getElementById("AcResetButton").style.visibility = "hidden";
    document.getElementById("InaccountNatureAnchor").text = "\u2714 A/C Nature & Currency";


    if (var1 == "1") {
        document.getElementById("CiResetButton").style.visibility = "hidden";
        document.getElementById("InContactInformationAnchor").text = "\u2714 Contact Information";


    }



    if (var2 == "1") {
        document.getElementById("AuthResetButton").style.visibility = "hidden";
        document.getElementById("InAuthorizedPersonAnchor").text = "\u2714 Authorized Persons";
        console.log(var2);


    }

    if (var3 == "1") {

        document.getElementById("AuResetButton").style.visibility = "hidden";
        document.getElementById("InOperatingInstructionAnchor").text = "\u2714 Operating Instructions";



    }

    if (var4 == "1") {

        document.getElementById("KnResetButton").style.visibility = "hidden";
        document.getElementById("InKnowYourCustomerAnchor").text = "\u2714 Know Your Customer";


    }

    if (var5 == "1") {

        document.getElementById("CdResetButton").style.visibility = "hidden";
        document.getElementById("InCertDepositInfoAnchor").text = "\u2714 Certificate Depostit Infor";


    }

    if (var6 == "1") {
        document.getElementById("DrResetButton").style.visibility = "hidden";
        document.getElementById("InDocumentRequiredAnchor").text = "\u2714 Document Required";

    }

    if (var7 == "1")
    {
        document.getElementById("WhoBtnReset").style.visibility = "hidden";
        document.getElementById("InWhoAuthorizedAnchor").text = "\u2714 Who Authorized";
    }

}


function IndividualAccountOpenPendingAlert(var1, var2, var3, var4, var5, var6, var7, mesg) {
    if (mesg == "null") {
        $('#alerts').hide();
    }
    else {
        $('#alerts').append(
               '<div class="alert alert-success">' +
                   '<button type="button" class="close" data-dismiss="alert">' +
                   '&times;</button>' + mesg + '</div>');

    }


    document.getElementById("AcResetButton").style.visibility = "hidden";
    document.getElementById("InaccountNatureAnchor").text = "\u2714 A/C Nature & Currency";
    //alert("Heelo");
   // $('.nav-tabs a[href="#sectionb"]').tab('show');

    if (var1 == "1") {
        document.getElementById("ApResetButton").style.visibility = "hidden";
        document.getElementById("InApplicantInformationAnchor").text = "\u2714 Applicant Information";


    }



    if (var2 == "1") {
        document.getElementById("AdResetButton").style.visibility = "hidden";
        document.getElementById("InAddressInformationAnchor").text = "\u2714 Address Information";
        console.log(var2);


    }

    if (var3 == "1") {

        document.getElementById("AuResetButton").style.visibility = "hidden";
        document.getElementById("InOperatingInstructionAnchor").text = "\u2714 Operating Instructions";



    }

    if (var4 == "1") {

        document.getElementById("NkResetButton").style.visibility = "hidden";
        document.getElementById("InNextOfKinAnchor").text = "\u2714 Next Of Kin Info";


    }

    if (var5 == "1") {

        document.getElementById("KnResetButton").style.visibility = "hidden";
        document.getElementById("InKnowYourCustomerAnchor").text = "\u2714 Know Your Customer";


    }

    if (var6 == "1") {
        document.getElementById("CdResetButton").style.visibility = "hidden";
        document.getElementById("InCertDepositInfoAnchor").text = "\u2714 Certificate Deposit Info";

    }

    if (var7 == "1") {
        document.getElementById("DrResetButton").style.visibility = "hidden";
        document.getElementById("InDocumentRequiredAnchor").text = "\u2714 Document Required";

    }


}

function openModal() {
    // alert("hello");

    //  $('#InBasicInfoAnchor').html("abc");

    $("#myModal").modal();

}

function func1() {

    //$('#contactInformation').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();


    //$('#otherIdentity').removeClass("active");
    //$('#basicInformation').removeClass("active");


}


function func2() {

    //$('#employmentInformation').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();


    //$('#contactInformation').removeClass("active");
    //$('#basicInformation').removeClass("active");


}


function func3() {

    //$('#miscInfo').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();


    //$('#employmentInformation').removeClass("active");
    //$('#basicInformation').removeClass("active");


}


function func4() {

    //$('#bankRelation').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();


    //$('#miscInfo').removeClass("active");
    //$('#basicInformation').removeClass("active");


}


function func5() {

    //$('#fatcaIdentification').addClass("active");

    $('#otherIdentity').show();
    $('#contactInformation').show();
    $('#employmentInformation').show();
    $('#miscInfo').show();
    $('#bankRelation').show();
    $('#fatcaIdentification').show();


    //$('#bankRelation').removeClass("active");
    //$('#basicInformation').removeClass("active");

}


function showNextOfKinAlert(mesg) {

    $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');

}

var resetBasicInfo = 0;

function showAlertFatcaIn(var1, var2, var3, var4, var5, mesg) {


    $('#alerts').append(
    '<div class="alert alert-success">' +
        '<button type="button" class="close" data-dismiss="alert">' +
        '&times;</button>' + mesg + '</div>');


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";


    document.getElementById("InResetFatca").style.visibility = "hidden";
    document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";



    if (var1 == "1") {

        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    }

    if (var2 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var3 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";


    }

    if (var4 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";


    }

    if (var5 == "1") {
        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";

    }


}


function showAlertBankRelationIn(var1, var2, var3, var4, var5, mesg) {

    $('#alerts').append(
    '<div class="alert alert-success">' +
        '<button type="button" class="close" data-dismiss="alert">' +
        '&times;</button>' + mesg + '</div>');


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    document.getElementById("InResetBankRelation").style.visibility = "hidden";
    document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";



    if (var1 == "1") {

        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    }

    if (var2 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var3 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";


    }

    if (var4 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";


    }

    if (var5 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }


}

function showAlertMiscInfoIn(var1, var2, var3, var4, var5, mesg) {
    $('#alerts').append(
     '<div class="alert alert-success">' +
         '<button type="button" class="close" data-dismiss="alert">' +
         '&times;</button>' + mesg + '</div>');


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    document.getElementById("InResetMiscInfo").style.visibility = "hidden";
    document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";



    if (var1 == "1") {

        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    }

    if (var2 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var3 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";


    }

    if (var4 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }

    if (var5 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }

}

function showAlertEmployInfoIn(var1, var2, var3, var4, var5, mesg) {

    $('#alerts').append(
      '<div class="alert alert-success">' +
          '<button type="button" class="close" data-dismiss="alert">' +
          '&times;</button>' + mesg + '</div>');


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";


    document.getElementById("InResetEmployInfo").style.visibility = "hidden";
    document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";



    if (var1 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var2 == "1") {
        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";


    }

    if (var3 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";
    }

    if (var4 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }

    if (var5 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }


}

function showAlertContactInfoIn(var1, var2, var3, var4, var5, mesg) {
    $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    document.getElementById("InResetContactInfo").style.visibility = "hidden";
    document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";



    if (var1 == "1") {
        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    }

    if (var2 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";

    }

    if (var3 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";
    }

    if (var4 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }

    if (var5 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }

}

function showAlertIdentIn(var1, var2, var3, var4, var5, mesg) {
    // alert("Hello");
    // console.log(mesg);

    $('#alerts').append(
   '<div class="alert alert-success">' +
       '<button type="button" class="close" data-dismiss="alert">' +
       '&times;</button>' + mesg + '</div>');





    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
    document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    if (var1 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";
    }

    if (var2 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";

    }

    if (var3 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";
    }

    if (var4 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }

    if (var5 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }
}


function IndividualAfterPendingAlert(var1, var2, var3, var4, var5, var6, mesg) {
    if (mesg == "null") {
        $('#alerts').hide();
    }
    else {
        $('#alerts').append(
               '<div class="alert alert-success">' +
                   '<button type="button" class="close" data-dismiss="alert">' +
                   '&times;</button>' + mesg + '</div>');

    }


    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    // $('.nav-tabs a[href="#sectionb"]').tab('show');


    if (var1 == "1") {
        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";


    }

    if (var2 == "1") {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";



    }

    if (var3 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";



    }

    if (var4 == "1") {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";


    }

    if (var5 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";


    }

    if (var6 == "1") {
        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";

    }


}

function BusinessAfterPendingAlert(var1, var2, var3, var4, var5, var6,var7, mesg) {
    if (mesg == "null") {
        $('#alerts').hide();
    }
    else {
        $('#alerts').append(
               '<div class="alert alert-success">' +
                   '<button type="button" class="close" data-dismiss="alert">' +
                   '&times;</button>' + mesg + '</div>');

    }



    document.getElementById("BusbtnResetBasicInfo").style.visibility = "hidden";
    document.getElementById("BusBasicInfoAnchor").text = "\u2714 Basic Information";


    if (var1 == "1") {

        document.getElementById("BusCiResetButton").style.visibility = "hidden";
        document.getElementById("BusContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var2 == "1") {


        document.getElementById("BusHoResetButton").style.visibility = "hidden";
        document.getElementById("BusHeadContactInfoAnchor").text = "\u2714 Head Office Address/Contact Information";


    }

    if (var3 == "1") {

        document.getElementById("BusBdResetButton").style.visibility = "hidden";
        document.getElementById("BusBusinessDetailAnchor").text = "\u2714 Business Detail";



    }

    if (var4 == "1") {


        document.getElementById("BusBrResetButton").style.visibility = "hidden";
        document.getElementById("BusBankRelationAnchor").text = "\u2714 Banking Relationship";


    }

    if (var5 == "1") {

        document.getElementById("BusFiResetButton").style.visibility = "hidden";
        document.getElementById("BusFinanceInfoAnchor").text = "\u2714 Financial Relationship";


    }

    if (var6 == "1") {

        document.getElementById("BusShResetButton").style.visibility = "hidden";
        document.getElementById("BusShareHolderAnchor").text = "\u2714 Share Holders Information";

    }

    if (var7 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("BusFatcaAnchor").text = "\u2714 FATCA";

    }



}



function showAlertBasicInfoBus(mesg) {
    $('#alerts').append(
        '<div class="alert alert-success">' +
            '<button type="button" class="close" data-dismiss="alert">' +
            '&times;</button>' + mesg + '</div>');

    document.getElementById("BusbtnResetBasicInfo").style.visibility = "hidden";
    document.getElementById("BusBasicInfoAnchor").text = "\u2714 Basic Information";


}



function showAlertBasicInfoInd(mesg) {
    console.log(mesg);
    if (mesg == "null") {
        $('#alerts').hide();
    }
    else {
        $('#alerts').append(
           '<div class="alert alert-success">' +
               '<button type="button" class="close" data-dismiss="alert">' +
               '&times;</button>' + mesg + '</div>');

    }

    //alert("hello");
    //$('#alerts').show();

    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

}





function IndividualAfterPending(var1, var2, var3, var4, var5, var6) {

    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";


    if (var1 == "1") {

        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";


    }

    if (var2 == "1") {


        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";


    }

    if (var3 == "1") {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";



    }

    if (var4 == "1") {


        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";


    }

    if (var5 == "1") {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";


    }

    if (var6 == "1") {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";

    }
}







function checkButtonHidden(button, anchor, str) {

    alert(document.getElementById(button).style.visibility);
    if (document.getElementById(button).style.visibility == "hidden") {
        alert("Hello again");
        document.getElementById(anchor).text = str;

    }
}

function showAlertBusiness(a) {
    // alert("hello");

    $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');



    if (a == 1) {
        document.getElementById("BusbtnResetBasicInfo").style.visibility = "hidden";
    }
    else if (a == 2) {
        document.getElementById("CiResetButton").style.visibility = "hidden";
    }
    else if (a == 3) {
        document.getElementById("HoResetButton").style.visibility = "hidden";
    }
    else if (a == 4) {
        document.getElementById("BdResetButton").style.visibility = "hidden";
    }
    else if (a == 5) {
        document.getElementById("BrResetButton").style.visibility = "hidden";
    }
    else if (a == 6) {
        document.getElementById("FiResetButton").style.visibility = "hidden";

    }
}

function myAlert() {
    bootbox.alert("Hello world!", function () {
        Example.show("Hello world callback");
    });
}


function hideIndividualAll(i) {
    if (i == 1) {
        document.getElementById("InResetBasicInfo").style.visibility = "hidden";
        document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";
    }
    else if (i == 2) {

        document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
        document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    }
    else if (i == 3) {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";
    }
    else if (i == 4) {
        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";

    }
    else if (i == 5) {
        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";

    }
    else if (i == 6) {
        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }

    else {
        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";

    }
}


function BusBaisInfoReset() {

}

function BioMetric(success, mesg) {

    if (success == "1") {
        $('#alerts').append(
		'<div class="alert alert-success">' +
			'<button type="button" class="close" data-dismiss="alert">' +
			'&times;</button>' + mesg + '</div>');
    }
    else {
        $('#alerts').append(
		'<div class="alert alert-danger">' +
			'<button type="button" class="close" data-dismiss="alert">' +
			'&times;</button>' + mesg + '</div>');
    }


}

function funcbasicInfoReset() {
    document.getElementById("lstCifType").selectedIndex = 0;
    document.getElementById("lstPrimaryDocumentType").selectedIndex = 0;

    document.getElementById("lstTitle").selectedIndex = 0;

    document.getElementById("lstTitleFather").selectedIndex = 0;

    document.getElementById("lstCOB").selectedIndex = 0;

    document.getElementById("lstMartialStatus").selectedIndex = 0;

    document.getElementById("lstGender").selectedIndex = 0;

    document.getElementById("lstReligion").selectedIndex = 0;

    document.getElementById("lstResident").selectedIndex = 0;

    document.getElementById("lstCOR").selectedIndex = 0;

    document.getElementById("lstCustomerDeals").selectedIndex = 0;

    document.getElementById("txtCnic").value = "";
    document.getElementById("txtName").value = "";
    document.getElementById("txtFatherName").value = "";
    document.getElementById("txtFatherCnic").value = "";
    document.getElementById("txtFatherCif").value = "";
    document.getElementById("txtMotherName").value = "";
    document.getElementById("txtMotherCnic").value = "";
    document.getElementById("txtMotherCnicOld").value = "";
    document.getElementById("txtBithPlace").value = "";
    document.getElementById("txtIncome").value = "";

    var dt = new Date();
    //alert(dt.getFullYear());

    var dd = (dt.getMonth() + 1);

    alert(dd + '/' + dt.getDate() + '/' + dt.getFullYear());

    var id = "<%= txtDOB.ClientID %>";
    document.getElementById("txtDOB").value = dd + '/' + dt.getDate() + '/' + dt.getFullYear();

}