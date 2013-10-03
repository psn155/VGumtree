/// <reference path="../angular.js" />

// adsViewController
var adsViewController = function ($rootScope, $scope, dataFactory, categoryFactory, locationFactory, imageFactory) {
    
    $rootScope.data = dataFactory.getInternalAds();

    if (!$rootScope.data || $rootScope.data.length == 0) {
        dataFactory.getAds()
        .success(function (result) {
            $rootScope.data = result;
            dataFactory.applyAds(result);
            dataFactory.resetAds();
        })
        .error(function (result) {
            alert("Error loading ads!");
        });
    }  
    
};

// adCreateController
var adCreateController = function ($scope, dataFactory, $window, attributeFactory, categoryFactory, googleGeoApi, fileReader, imageFactory) {
    $scope.newAd = {};
    $scope.selectedCategoryId = null;
    $scope.selectedSubCategoryId = null;
    $scope.attributes = [];

    $scope.categories = [];
    $scope.subCategories = [];
    
    $scope.formattedAddresses = [];
    $scope.selectedAddress = {};

    $scope.uploadPhotos = [];
    $scope.photos = [];

    // Get all categories
    getCategories($scope, categoryFactory);
        
    // Get images
    // Listen for the file selected event
    $scope.$on("fileSelected", function (event, args) {
        $scope.$apply(function () {
            //$scope.imageSrc = args.file;
            //$scope.photos.push(args.file);
            fileReader.readAsDataUrl(args.file, $scope)
                .then(function (result) {
                    $scope.imageSrc = result;
                    $scope.photos.push(result);
                    $scope.uploadPhotos.push(args.file);
                });
        });
    });   


    $scope.updateImage = function (index) {
        $scope.imageSrc = $scope.photos[index];
    }

    $scope.deleteImage = function (index) {
        $scope.photos.splice(index, 1);
        $scope.uploadPhotos.splice(index, 1);
    }






    // Function to get all attributes for a specific sub category Id
    $scope.updateAttributes = function () {
        clearAdditionalInfor($scope);        

        if ($scope.selectedSubCategoryId != null) {
            attributeFactory.getAttrsBySubCatId($scope.selectedSubCategoryId)
                .success(function (result) {
                    if (result.length > 0) {
                        //sleep(5000);
                        $.each(result, function (i, item) {
                            $scope.attributes.push({
                                Id: item.Id,
                                Name: item.Name,
                                Value: null
                            });
                        });
                    }
                })
                .error(function () {
                    alert("Error loading attribures");
                });
        }
    }

    $scope.resetSubCategoryId = function () {
        $scope.selectedSubCategoryId = null;
        clearAdditionalInfor($scope);
    }

    // Get formatted addresses from Google api
    $scope.getGoogleAddresses = function () {
        if ($scope.StreetAddress || $scope.District || $scope.Province || $scope.Country) {
            var enteredAddr = $scope.StreetAddress + ", " + $scope.District + ", " + $scope.Province + ", " + $scope.Country;
    
            // Replace spaces by '+'
            var requestAddr = enteredAddr.replace(/\ /g, '+');

            googleGeoApi.getJson(requestAddr)
            .success(function (result) {
                //alert("Google api: " + result.status + " " + result.results[0].formatted_address);
                var location = {};
                if (result.status == "OK") {
                    $scope.formattedAddresses = result.results;
                }
            })
            .error(function () {
                alert("Cannot validate the address using Google!");
            });
        } else {
            alert("Please enter an address!");
        }
    }
   
    $scope.save = function () {
        $scope.newAd.SubCategoryId = $scope.selectedSubCategoryId;
        
        if ($scope.StreetAddress || $scope.District || $scope.Province || $scope.Country) {
            var location = {};
            location.EnteredAddress = $scope.StreetAddress + ", " + $scope.District + ", " + $scope.Province + ", " + $scope.Country;;

            if ($scope.selectedAddress != null) {
                location.FormattedAddress = $scope.selectedAddress.formatted_address;
                location.Latitude = $scope.selectedAddress.geometry.location.lat;
                location.Longtitude = $scope.selectedAddress.geometry.location.lng;

                // Look for the administrative_area_level_2 in the Google geocode result
                var adminAreaLevel2_longName = null;
                var adminAreaLevel1_longName = null;
                var locality = null;
                for (i = 0; i < $scope.selectedAddress.address_components.length; i++) {
                    if ($scope.selectedAddress.address_components[i].types[0] == "administrative_area_level_2") {
                        adminAreaLevel2_longName = $scope.selectedAddress.address_components[i].long_name;
                    }
                    if ($scope.selectedAddress.address_components[i].types[0] == "administrative_area_level_1") {
                        adminAreaLevel1_longName = $scope.selectedAddress.address_components[i].long_name;
                    }
                    if ($scope.selectedAddress.address_components[i].types[0] == "locality") {
                        locality = $scope.selectedAddress.address_components[i].long_name;
                    }
                }

                if (adminAreaLevel2_longName == null) {
                    adminAreaLevel2_longName = locality;
                }

                location.AdminAreaLevel2 = {
                    Name: adminAreaLevel2_longName,
                    AdminAreaLevel1: {
                        Name: adminAreaLevel1_longName,
                        CountryId: 1 // TO DO: currently hardoced this value for Vietnam. If there are more countries, need to change this to dynamic value based on use inputs
                    }
                }
            }

            $scope.newAd.Locations = [];
            $scope.newAd.Locations.push(location);
        }
        
        if ($scope.attributes.length > 0) {
            var adAttrsObject = [];
            $.each($scope.attributes, function (i, item) {
                if (item.Value != null) {
                    adAttrsObject.push({
                        AdId: 0, // Initialized this as zero. Needs to assign the correct adId in the webAPI after creating the ad
                        AttributeId: item.Id,
                        Value: item.Value
                    });
                };
            });

            $scope.newAd.AdAttributes = adAttrsObject;
        }

        dataFactory.addAd($scope.newAd)
        .success(function (result) {          

            dataFactory.pushAd(result);

            // Upload images 
            if ($scope.uploadPhotos.length > 0) {
                // Upload photos one by one, each photo in each request
                //$.each($scope.uploadPhotos, function (i, photo) {
                //    imageFactory.postPhoto(photo, i, result.Id)
                //    .success(function (uploadResult) {
                //        // TODO: show something
                //        //$window.location = "#/";
                //    })
                //    .error(function (uploadResult) {
                //        alert("Error uploading photos!");
                //    });
                //});

                // Upload photos all in one request
                imageFactory.postPhotos($scope.uploadPhotos, result.Id)
                .success(function (uploadResult) {
                    // Get all ads from server again after successfully creating new ad
                    dataFactory.getAds()
                        .success(function (result) {
                            dataFactory.applyAds(result);
                            dataFactory.resetAds();
                        })
                        .error(function (result) {
                            alert("Error loading ads!");
                        });
                    $window.location = "#/";
                })
                .error(function (uploadResult) {
                    alert("Error uploading photos!");
                });
            } else {
                $window.location = "#/";
            }
        })
        .error(function () {
            alert("Error saving ad!");
        });
    }
};

// singleAdController
var singleAdController = function ($rootScope, $scope, dataFactory, $routeParams, attributeFactory, $window, imageFactory) {
    $scope.ad = dataFactory.getInternalAdById($routeParams.id);
    $scope.attributes = [];
    $scope.imageSrc = "";
        
    getImageUploadFolder($scope, imageFactory);
    
    $scope.hasImages = function () {
        if ($scope.ad != null) {
            if ($scope.ad.Photos != null && $scope.ad.Photos.length > 0) {                
                return true;
            } else {
                $("#main-image").empty(); // If there is no images, delete the main-image div so that it does not make a failed request to the server to an empty source
                $("#thumbnail-image").empty();
            }
        }        
        return false;
    }

    if ($scope.ad == null) {
        dataFactory.getAdById($routeParams.id)
            .success(function (result) {
                $scope.ad = result;
                getAttributesByAdId($scope, $scope.ad.Id, attributeFactory);
                if ($scope.hasImages()) {
                    $scope.imageSrc = result.Photos[0].Url;
                }
                $scope.canDelete = $rootScope.userId == 1 || $rootScope.userId == $scope.ad.UserId;
            })
            .error(function () {
                alert("Error loading ad!");
            });
    } else {
        getAttributesByAdId($scope, $scope.ad.Id, attributeFactory);
        if ($scope.hasImages()) {
            $scope.imageSrc = $scope.ad.Photos[0].Url;
        }
        $scope.canDelete = $rootScope.userId == 1 || $rootScope.userId == $scope.ad.UserId;
    }

    

    $scope.updateImage = function (index) {
        $scope.imageSrc = $scope.ad.Photos[index].Url;
    }
  

    $scope.delete = function () {
        dataFactory.deleteAd($scope.ad.Id)
        .success(function (result) {
            dataFactory.removeAd($scope.ad);
            $window.location = "#/";
        })
        .error(function () {
            alert("Error deleting ad!");
        });
    }

    $scope.showPhone = function () {
        dataFactory.getFullPhoneNumber($scope.ad.Id)
        .success(function (result) {            
            $scope.ad.ContactPhone = trimQuotes(result);
        })
        .error(function () {
            alert("Error getting phone number!");
        });
    }
        
};


// myAdsController
var myAdsController = function ($rootScope, $scope, dataFactory, attributeFactory, imageFactory) {
    $rootScope.myAds = dataFactory.getInternalMyAds();

    if (!$rootScope.myAds || $rootScope.myAds.length == 0) {
        dataFactory.getMyAds()
        .success(function (result) {
            $rootScope.myAds = result;
            dataFactory.applyMyAds(result);
        })
        .error(function (result) {
            alert("Error loading my ads!");
        });
    }    
};

// contactController
var contactController = function () {

}








function clearAdditionalInfor($scope) {
    $(".additionalInfor").empty();
    $scope.attributes = [];
}

function getSubCategoriesByCatId($scope, categoryFactory, catId) {
    $scope.categories = categoryFactory.getInternalCategories();

    if (!$scope.categories || $scope.categories.length == 0) {
        categoryFactory.getCategories()
        .success(function (result) {
            $scope.categories = result;
            categoryFactory.applyCategories(result);
        })
        .error(function (result) {
            alert("Error loading categories!");
        });
    }
}


function getAttributesByAdId($scope, adId, attributeFactory) {
    attributeFactory.getAttrsByAdId(adId)
        .success(function (result) {
            $scope.attributes = result;
        })
        .error(function () {
            alert("Error loading attribures");
        });
}

function sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}

function trimQuotes(value) {
    value = value.replace(/\"/g, '');    
    return value;
}









