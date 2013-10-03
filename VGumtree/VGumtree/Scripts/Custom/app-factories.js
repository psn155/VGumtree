// dataFactory: Main factory for getting Ads data
adAppModule.factory("dataFactory", ["$http", function ($http) {
    var urlBase = "/api/AdAPI",

        _ads = [],

        _myAds = [],

        _reset = false,

        _applyAds = function (retrievedAds) {
            _ads = retrievedAds;
        },

        _applyMyAds = function (retrievedMyAds) {
            _myAds = retrievedMyAds;
        },

        _getInternalAds = function () {
            return _ads;
        },

        _getInternalMyAds = function () {
            return _myAds;
        },

        _getInternalAdById = function (id) {
            var found = null;

            // TO-DO: need to use faster look up here
            if (_ads.length > 0) {
                $.each(_ads, function (i, item) {
                    if (item.Id == id) {
                        found = item;
                        return false;
                    }
                });
            }

            return found;
        },

        _pushAd = function (ad) {
            if (_ads.length > 0) {
                _ads.push(ad);
            } else {
                _getAds()
                    .success(function (result) {
                        _applyAds(result);
                        _ads.push(ad);
                    })
                    .error(function (result) {
                        alert("Cannot get ads");
                    });
            }
        },

        _removeAd = function (ad) {
            var index = _ads.indexOf(ad);
            if (index >= 0) {
                _ads.splice(index, 1);
            }
        },

        _getAds = function () {
            return $http.get(urlBase);
        },

        _getAdById = function (id) {
            return $http.get(urlBase + "/" + id);
        },

        _getAdsBySearchCriteria = function (catId, subCatId, keyword, adminAreaLevel1Id, adminAreaLevel2Id) {
            return $http.get(urlBase + "?catId=" + catId + "&subCatId=" + subCatId + "&adminAreaLevel1Id=" + adminAreaLevel1Id + "&adminAreaLevel2Id=" + adminAreaLevel2Id + "&keyword=" + keyword);
        },

        _getFullPhoneNumber = function (adId) {
            return $http.get(urlBase + "?id=" + adId + "&fullPhoneNumber=true");
        },

        _getMyAds = function () {
            return $http.get(urlBase + "?myAds=true");
        },

        _addAd = function (ad) {
            return $http.post(urlBase, ad);
        },

        _updateAd = function (ad) {
            return $http.put(urlBase + "/" + ad.id, ad);
        },

        _deleteAd = function (id) {
            return $http.delete(urlBase + "/" + id);
        },

        _resetAds = function () {
            if (!_reset) {
                setInterval(function () { _ads = []; }, 600000);
                _reset = true;
            }
        },

        _isReset = function () {
            return _reset;
        };

    return {
        getInternalAds: _getInternalAds,
        getInternalAdById: _getInternalAdById,
        applyAds: _applyAds,
        pushAd: _pushAd,
        removeAd: _removeAd,
        getAds: _getAds,
        getAdById: _getAdById,
        getAdsBySearchCriteria: _getAdsBySearchCriteria,
        getFullPhoneNumber: _getFullPhoneNumber,
        getMyAds: _getMyAds,
        getInternalMyAds: _getInternalMyAds,
        applyMyAds: _applyMyAds,
        addAd: _addAd,
        updateAd: _updateAd,
        deleteAd: _deleteAd,
        resetAds: _resetAds
    }
}]);

// imageFactory
adAppModule.factory("imageFactory", ["$http", function ($http) {
    var urlBase = "/api/PhotoAPI",

        _imageUploadFolder = "",

        _getInternalImageUploadFolder = function () {
            return _imageUploadFolder;
        },

        _getImageUploadFolder = function () {
            return $http.get(urlBase + "?uploadFolder=true");
        },

        _applyImageUploadFolder = function (folder) {
            _imageUploadFolder = folder;
        },

        _postPhotos = function (photos, id) {
            return $http({
                method: 'POST',
                url: urlBase + "/PostPhotos",
                //IMPORTANT!!! You might think this should be set to 'multipart/form-data' 
                // but this is not true because when we are sending up files the request 
                // needs to include a 'boundary' parameter which identifies the boundary 
                // name between parts in this multi-part request and setting the Content-type 
                // manually will not set this boundary parameter. For whatever reason, 
                // setting the Content-type to 'false' will force the request to automatically
                // populate the headers properly including the boundary parameter.
                headers: { 'Content-Type': false },
                //This method will allow us to change how the data is sent up to the server
                // for which we'll need to encapsulate the model data in 'FormData'
                transformRequest: function (data) {
                    var formData = new FormData();
                    //need to convert our json object to a string version of json otherwise
                    // the browser will do a 'toString()' on the object which will result 
                    // in the value '[Object object]' on the server.
                    formData.append("id", angular.toJson(data.id));
                    //now add all of the assigned files
                    for (var i = 0; i < data.files.length; i++) {
                        //add each file to the form data and iteratively name them
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { id: id, files: photos }
            });
        },

        _postPhoto = function (photo, photoId, adId) {
            return $http({
                method: 'POST',
                url: urlBase + "/PostPhoto",
                //IMPORTANT!!! You might think this should be set to 'multipart/form-data' 
                // but this is not true because when we are sending up files the request 
                // needs to include a 'boundary' parameter which identifies the boundary 
                // name between parts in this multi-part request and setting the Content-type 
                // manually will not set this boundary parameter. For whatever reason, 
                // setting the Content-type to 'false' will force the request to automatically
                // populate the headers properly including the boundary parameter.
                headers: { 'Content-Type': false },
                //This method will allow us to change how the data is sent up to the server
                // for which we'll need to encapsulate the model data in 'FormData'
                transformRequest: function (data) {
                    var formData = new FormData();
                    //need to convert our json object to a string version of json otherwise
                    // the browser will do a 'toString()' on the object which will result 
                    // in the value '[Object object]' on the server.
                    formData.append("photoId", angular.toJson(data.photoId));
                    formData.append("adId", angular.toJson(data.adId));
                    formData.append("file" + 0, data.file);
                    return formData;
                },
                //Create an object that contains the model and files which will be transformed
                // in the above transformRequest method
                data: { photoId: photoId, adId: adId, file: photo }
            });
        };

    return {
        postPhotos: _postPhotos,
        postPhoto: _postPhoto,
        getImageUploadFolder: _getImageUploadFolder,
        getInternalImageUploadFolder: _getInternalImageUploadFolder,
        applyImageUploadFolder: _applyImageUploadFolder
    }
}]);

// googleGeoApi - Google Geocode api
adAppModule.factory("googleGeoApi", ["$http", function ($http) {
    var urlBase = "http://maps.googleapis.com/maps/api/geocode/json?address=",

        params = "&sensor=false&language=vi",

        _getJson = function (address) {
            $http.defaults.useXDomain = true;
            delete $http.defaults.headers.common['X-Requested-With'];
            return $http.get(urlBase + address + params);
        };

    return {
        getJson: _getJson
    }

}]);

// categoryFactory
adAppModule.factory("categoryFactory", ["$http", function ($http) {
    var urlBase = "/api/categoryapi",

        _categories = [],

        _subCategories = [],

        _applyCategories = function (retrievedCats) {
            _categories = retrievedCats;
        },

        _applySubCategories = function (retrievedSubCats) {
            _subCategories = retrievedSubCats;
        },

        _getInternalCategories = function () {
            return _categories;
        },

        _getInternalSubCategories = function () {
            return _subCategories;
        },

        _getCategories = function () {
            return $http.get(urlBase);
        },

        _getSubCategories = function () {
            return $http.get(urlBase + "?subCatOnly=true");
        };

    return {
        getCategories: _getCategories,
        getSubCategories: _getSubCategories,
        applyCategories: _applyCategories,
        applySubCategories: _applySubCategories,
        getInternalCategories: _getInternalCategories,
        getInternalSubCategories: _getInternalSubCategories
    }
}]);

// attributeFactory
adAppModule.factory("attributeFactory", ["$http", function ($http) {
    var urlBase = "/api/AttrApi",

        _getAttrsBySubCatId = function (subCatId) {
            return $http.get(urlBase + "?subCatId=" + subCatId);
        },

        _getAttrsByAdId = function (adId) {
            return $http.get(urlBase + "?adId=" + adId);
        };

    return {
        getAttrsBySubCatId: _getAttrsBySubCatId,
        getAttrsByAdId: _getAttrsByAdId
    }
}]);

// attributeFactory
adAppModule.factory("locationFactory", ["$http", function ($http) {
    var urlBase = "/api/LocationAPI",

        _countries = [],

        _adminAreaLevel2s = [],

        _adminAreaLevel1s = [],

        _getInternalCountries = function () {
            return _countries;
        },

        _getInternalAdminAreaLevel2s = function () {
            return _adminAreaLevel2s;
        },

        _getInternalAdminAreaLevel1s = function () {
            return _adminAreaLevel1s;
        },

        _getCountries = function () {
            return $http.get(urlBase + "?country=true");
        },

        _getAdminAreaLevel2s = function () {
            return $http.get(urlBase + "?adminAreaLevel2=true");
        },

        _getAdminAreaLevel1s = function () {
            return $http.get(urlBase + "?adminAreaLevel1=true");
        },

        _applyCountries = function (retrievedCountries) {
            _countries = retrievedCountries;
        },

        _applyAdminAreaLevel2s = function (retrievedAdminAreaLevel2s) {
            _adminAreaLevel2s = retrievedAdminAreaLevel2s;
        },

        _applyAdminAreaLevel1s = function (retrievedAdminAreaLevel1s) {
            _adminAreaLevel1s = retrievedAdminAreaLevel1s;
        };

    return {
        getInternalCountries: _getInternalCountries,
        getInternalAdminAreaLevel2s: _getInternalAdminAreaLevel2s,
        getInternalAdminAreaLevel1s: _getInternalAdminAreaLevel1s,
        getCountries: _getCountries,
        getAdminAreaLevel2s: _getAdminAreaLevel2s,
        getAdminAreaLevel1s: _getAdminAreaLevel1s,
        applyCountries: _applyCountries,
        applyAdminAreaLevel2s: _applyAdminAreaLevel2s,
        applyAdminAreaLevel1s: _applyAdminAreaLevel1s
    }
}]);

// file reader factory
adAppModule.factory("fileReader",
                   ["$q", "$log", function ($q, $log) {

                       var onLoad = function (reader, deferred, scope) {
                           return function () {
                               scope.$apply(function () {
                                   deferred.resolve(reader.result);
                               });
                           };
                       };

                       var onError = function (reader, deferred, scope) {
                           return function () {
                               scope.$apply(function () {
                                   deferred.reject(reader.result);
                               });
                           };
                       };

                       var onProgress = function (reader, scope) {
                           return function (event) {
                               scope.$broadcast("fileProgress",
                                   {
                                       total: event.total,
                                       loaded: event.loaded
                                   });
                           };
                       };

                       var getReader = function (deferred, scope) {
                           var reader = new FileReader();
                           reader.onload = onLoad(reader, deferred, scope);
                           reader.onerror = onError(reader, deferred, scope);
                           reader.onprogress = onProgress(reader, scope);
                           return reader;
                       };

                       var readAsDataURL = function (file, scope) {
                           var deferred = $q.defer();

                           var reader = getReader(deferred, scope);
                           reader.readAsDataURL(file);

                           return deferred.promise;
                       };

                       return {
                           readAsDataUrl: readAsDataURL
                       };
                   }]);
