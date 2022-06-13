$(document).ready(function () {

    //Javascript for Header 

    // Create Claim Header
    $('#headnew').click(function () {
        $('#depddl').removeAttr("disabled");
        $('#locddl').removeAttr("disabled");
        $('#depddl').val(null);
        $('#locddl').val(null);
        GenNum();
        GetDeparment();
        alert('Claim Header Created');
    });

    // Clear Header Form
    $('#headclear').click(function () {
        $('#depddl').val(null);
        $('#locddl').val(null);
        $('#gennum').attr('disabled', true);
        $('#headsave').attr('disabled', true);
        $('#headclear').attr('disabled', true);
        alert('Form Cleared');
    });

    // Save Header Form
    $('#headsave').click(function () {
        $('#headclear').attr('disabled', true);
        $('#gennum').attr('disabled', true);
        $('#locddl').attr('disabled', true);
        $('#depddl').attr('disabled', true);
        $('#headsave').attr('disabled', true);
        GetUser();
        SaveHeader();
    });

    // Make User Have to Fill both DDL
    $("#locddl").change(function () {


        if ($("#depddl").val() != '' && $("#locddl").val() != '') {
            $('#headsave').removeAttr("disabled");
            $('#headclear').removeAttr("disabled");
        } else {
            $('#headsave').attr('disabled', true);
            $('#headclear').attr('disabled', true);
        }
    });

    // Make User Have to Fill Both DDL
    $("#depddl").change(function () {

        if ($("#locddl").val() != '' && $("#depddl").val() != '') {
            $('#headsave').removeAttr("disabled");
            $('#headclear').removeAttr("disabled");
        } else {
            $('#headsave').attr('disabled', true);
            $('#headclear').attr('disabled', true);
        }
    });

    //Javascript for Detail 

    // Create Claim Detail
    $('#detnew').click(function () {
        alert('Claim Detail Created');
    });

    // Save Detail
    $('#detsave').click(function () {
        SaveDetail();
    });

    // Claim Dropdown List
    $("#claimddl").change(function () {

        if ($("#claimddl").val == 'Makan') {
            $('#amountform').removeAttr("hidden");
            GetMakan();
        } else if ($("#claimddl").val == 'Kilometer') {
            $('#vehicleform').removeAttr("hidden");
            $('#amountform').removeAttr("hidden");
        } else if ($("#claimddl").val == 'Toll') {
            $('#amountform').removeAttr("hidden");
            $('#amount').removeAttr("disabled");
        } else {
            $('#amountform').attr('hidden', true);
            $('#vehicleform').attr('hidden', true);
            $('#vehicle').val(null);
            $('#vehicletype').val(null);
            $('#quanitity').val(null);
            $('#amount').val(null);
        }

        if ($("#claimddl").val() != '' && $("#userddl").val() != '' && $("#claimdate").val() != '') {
            $('#detsave').removeAttr("disabled");
        } else {
            $('#detsave').attr("disabled", true);

        }
    });

    // Get User Detailed Information
    $("#vehicle").change(function () {
        GetTransport();
    });

    // Get User Detailed Information
    $("#userddl").change(function () {
        GetUserInfo();
    });

    // Validation ClaimDate 30 Days
    $("#claimdate").change(function () {

        // d -> today, tempdate 0 -> claim date
        // end date -> convert claim date into javascript's date time
        // start date -> convert today date into javascript's date time
        // set hours -> bececause we picked date time, so we need to initialize the time into a fixed time

        var d = new Date();
        var tempdate = $("#claimdate").val();
        var enddate = new Date(tempdate);
        var startdate = new Date(d);
        startdate.setHours(7, 0, 0);

        // counted ->  date difference in DAYS
        // countedv2 -> simplify counted

        var counted = new Date(startdate.getTime() - enddate.getTime());

        // Formula to count Date Difference
        var countedv2 = Math.floor(counted / (1000 * 60 * 60 * 24));

        // Temp count -> convert countedv2 into integer, so it could be compared with other value

        var tempcount = parseInt(countedv2);

        // Claim Date validation

        if (tempcount > 30 || tempcount < 1) {
            $('#dateError').removeAttr("hidden");
            $('#claimdate').css("border-color", "red");
            $('#claimdate').val(null);
        } else {
            $('#dateError').attr('hidden', true);
            $('#claimdate').css("border-color", "");
        }
        GetClockIn();
    });

});