# ArcGIS Server REST API Wrapper #

This library provides a wrapper around the [ArcGIS Server REST API](http://help.arcgis.com/en/arcgisserver/10.0/apis/rest/index.html).  Note that not all operations have been implemented.  Operations will be added to the library on an as-needed basis.  (When the author needs to use an operation, it will be added.)  
Geocoding functionality was added in December 2017. Note that while the other operations in this library still use the older (version 10) ArcGIS Server REST API, the geocoding operations are based off the latest version of the [ArcGIS REST API](http://resources.arcgis.com/en/help/arcgis-rest-api/) [World Geocoding Service](https://developers.arcgis.com/rest/geocode/api-reference/overview-world-geocoding-service.htm). 
Currently `findAddressCandidates` is the only supported geocoding operation implemented in this library. 

## Unit Tests ##
You will need to add a valid `ClientId` and `ClientSecret` value to the app.config file. This file is omitted from the repository.
