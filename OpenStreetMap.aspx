<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenStreetMap.aspx.cs" Inherits="OpenStreetMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
      html, body, #basicMap {
          width: 100%;
          height: 100%;
          margin: 0;
      }
        </style>
    <script src="Scripts/OpenLayers-2.13.1/OpenLayers.js"></script>
    <script>

        var map, mappingLayer, vectorLayer, selectMarkerControl, selectedFeature;
        var fromProjection = new OpenLayers.Projection("EPSG:4326");   // Transform from WGS 1984
        var toProjection = new OpenLayers.Projection("EPSG:900913"); // to Spherical Mercator Projection
        function onFeatureSelect(feature) {
            selectedFeature = feature;
            var lat = selectedFeature.attributes.Lat;
            var lon = selectedFeature.attributes.Lon;
            popup = new OpenLayers.Popup.FramedCloud("tempId", feature.geometry.getBounds().getCenterLonLat(),
                                     null,
                                     " From Lat:" + selectedFeature.attributes.Lat + " Lon:" + selectedFeature.attributes.Lon,
                                     null, true);
            feature.popup = popup;
            map.addPopup(popup);


            //here handle funcking ReverseGeocode results
            getGeocodeResults(0, 0)
        }

        function onFeatureUnselect(feature) {
            map.removePopup(feature.popup);
            feature.popup.destroy();
            feature.popup = null;
        }

        function init() {
            map = new OpenLayers.Map('basicMap');
            mappingLayer = new OpenLayers.Layer.OSM("Simple OSM Map");

            map.addLayer(mappingLayer);


            vectorLayer = new OpenLayers.Layer.Vector("Vector Layer", { projection: "EPSG:4326" });
            selectMarkerControl = new OpenLayers.Control.SelectFeature(vectorLayer, { onSelect: onFeatureSelect, onUnselect: onFeatureUnselect });
            map.addControl(selectMarkerControl);

            selectMarkerControl.activate();
            map.addLayer(vectorLayer);

            map.setCenter(
                new OpenLayers.LonLat(51, 0).transform(
                    new OpenLayers.Projection("EPSG:4326"),
                    map.getProjectionObject())

                , 1
            );

            map.events.register('click', map, function (e) {

                var lonlat = map.getLonLatFromPixel(e.xy);
                lonlat1 = new OpenLayers.LonLat(lonlat.lon, lonlat.lat).transform(toProjection, fromProjection);

                placeMarker(lonlat1, map);
                Event.stop(e);
            });
        }


        function placeMarker(position, map) {

            var lat = position.lat
            var lon = position.lon

            var lonLat = new OpenLayers.Geometry.Point(lon, lat);

            lonLat.transform("EPSG:4326", map.getProjectionObject());
           
            var feature = new OpenLayers.Feature.Vector(lonLat, { Lat: lat, Lon: lon }
                                    );
            vectorLayer.addFeatures(feature);
            //var popup = new OpenLayers.Popup.FramedCloud("tempId", new OpenLayers.LonLat(randomLon, randomLat).transform("EPSG:4326", map.getProjectionObject()),
            //           null,
            //           randomFeature.attributes.salutation + " from Lat:" + randomFeature.attributes.Lat + " Lon:" + randomFeature.attributes.Lon,
            //           null, true);
            //randomFeature.popup = popup;
            //map.addPopup(popup);
        }

        function getGeocodeResults(lat, lon) {

            function reqListener() {
                alert('dupa');
            };
            var httpreq = "http://nominatim.openstreetmap.org/reverse?format=json&lat=-23.56320001&lon=-46.66140002"


            var req;
            if(window.XMLHttpRequest) {
                try {
                    req = new XMLHttpRequest();
                } catch(e) {
                    req = false;
                }
            } else if (window.ActiveXObject) {
                try {
                    req = new ActiveXObject("Msxml2.XMLHTTP");
                } catch (e) {
                    try {
                        req = new ActiveXObject("Microsoft.XMLHTTP");
                    } catch (e) {
                        req = false;
                    }
                }
            }

            req.onreadystatechange = processGeocodeResults;
            req.open("GET", httpreq, true);
            req.send(null);

            function processGeocodeResults() {
                if (req.readyState == 4 && req.status == 200) {
                    if (req.responseText == "Not found") {
                        alert('not found');
                    }
                    else {
                        var info = eval("(" + req.responseText + ")");

                        alert('found');
                    }
                }
                else
                    alert('process not successfull');
            }

        }

    </script>
</head>
    
 <body onload="init();">
    <div id="basicMap"></div>
  </body>
</html>