var myLatLng = { lat: -6.4, lng: 106.8186111 };
var mapOptions = {
    center: myLatLng,
    zoom: 7,
    mapTypeId: google.maps.MapTypeId.ROADMAP
};

var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);

var directionsService = new google.maps.DirectionsService();

var directionsDisplay = new google.maps.DirectionsRenderer();

directionsDisplay.setMap(map);

function calcRoute() {
    var request = {
        origin: document.getElementById("from").value,
        destination: document.getElementById("to").value,
        travelMode: google.maps.TravelMode.DRIVING,
        unitSystem: google.maps.UnitSystem.IMPERIAL
    }

    directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            $("#output").html("<div class= 'alert-info'>From:  ") + document.getElementById("from").value + ".<br />To: " + document.getElementById("to").value + "</div>"
            directionsDisplay.setDirections(result);

        } else {
            directionsDisplay.setDirections({ routes: [] });
            map.setCenter(myLatLng);

            $("#output").html("<div class= 'alert-danger'>Could not retrieve route</div> ");
        }
    });
}