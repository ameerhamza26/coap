

function showAlertBasicInfoInd2(){

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

function openModal()
{
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


function showNextOfKinAlert(mesg)
{

    $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');

}

var resetBasicInfo = 0;

function showAlertFatcaIn(var1,var2,var3,var4,var5,mesg)
{


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


function showAlertBankRelationIn(var1,var2,var3,var4,var5,mesg)
{

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

function showAlertMiscInfoIn(var1,var2,var3,var4,var5,mesg)
{
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

function showAlertEmployInfoIn(var1,var2,var3,var4,var5,mesg)
{

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

function showAlertContactInfoIn(var1,var2,var3,var4,var5,mesg)
{
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

function showAlertIdentIn(var1, var2,var3,var4,var5,mesg)
{
    // alert("Hello");
    console.log(mesg);
    if (mesg != "null")
    {
        $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');

    }

    

    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

    document.getElementById("InResetOtherIdentity").style.visibility = "hidden";
    document.getElementById("InOtherIdentityAnchor").text = "\u2714 CNIC/Other Identity";

    if (var1 == "1")
    {
        document.getElementById("InResetContactInfo").style.visibility = "hidden";
        document.getElementById("InContactInfoAnchor").text = "\u2714 Address/Contact Information";
    }
    
    if (var2 =="1")
    {

        document.getElementById("InResetEmployInfo").style.visibility = "hidden";
        document.getElementById("InEmployInfoAnchor").text = "\u2714 Employment Information";

    }

    if (var3 == "1")
    {

        document.getElementById("InResetMiscInfo").style.visibility = "hidden";
        document.getElementById("InMiscInfoAnchor").text = "\u2714 Miscellaneous Information";
    }

    if (var4=="1")
    {

        document.getElementById("InResetBankRelation").style.visibility = "hidden";
        document.getElementById("InBankRelationAnchor").text = "\u2714 Banking Relationship";
    }
   
    if (var5=="1")
    {

        document.getElementById("InResetFatca").style.visibility = "hidden";
        document.getElementById("InFatcaIdentAnchor").text = "\u2714 FATCA(U.S Person Identification)";
    }
}





function showAlertBasicInfoInd(mesg) {
    //console.log(mesg);
    //if (mesg == "null")
    //{
    //    $('#alerts').hide();
    //}
    //else {
    //    $('#alerts').append(
    //       '<div class="alert alert-success">' +
    //           '<button type="button" class="close" data-dismiss="alert">' +
    //           '&times;</button>' + mesg + '</div>');

    //}
    
    alert("hello");
    $('#alerts').show();

    document.getElementById("InResetBasicInfo").style.visibility = "hidden";
    document.getElementById("InBasicInfoAnchor").text = "\u2714 Basic Information";

}







   


   

function checkButtonHidden(button,anchor,str){
    
    alert(document.getElementById(button).style.visibility);
    if (document.getElementById(button).style.visibility == "hidden")
    {
        alert("Hello again");
        document.getElementById(anchor).text = str;

    }
}

function showAlertBusiness(a) 
{
    // alert("hello");

    $('#alerts').append(
       '<div class="alert alert-success">' +
           '<button type="button" class="close" data-dismiss="alert">' +
           '&times;</button>' + mesg + '</div>');



    if (a == 1)
    {
        document.getElementById("BusbtnResetBasicInfo").style.visibility = "hidden";
    }
    else if (a==2)
    {
        document.getElementById("CiResetButton").style.visibility = "hidden";
    }
    else if (a==3)
    {
        document.getElementById("HoResetButton").style.visibility = "hidden";
    }
    else if (a==4)
    {
        document.getElementById("BdResetButton").style.visibility = "hidden";
    }
    else if (a == 5) {
        document.getElementById("BrResetButton").style.visibility = "hidden";
    }
    else if (a==6)
    {
        document.getElementById("FiResetButton").style.visibility = "hidden";

    }
}

function myAlert()
{
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

    var dd = (dt.getMonth() +1);

    alert(dd + '/' + dt.getDate() + '/' + dt.getFullYear());
    
    var id = "<%= txtDOB.ClientID %>";
    document.getElementById("txtDOB").value = dd + '/' + dt.getDate() + '/' + dt.getFullYear();

}