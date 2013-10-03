/// <reference path="../angular.js" />

var adAppModule = angular.module("adAppModule", []);

// Routings
adAppModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "adsViewController",
        templateUrl: "Templates/adsView.html"
    })
    .when("/newad", {
        controller: "adCreateController",
        templateUrl: "Templates/adCreate.html"
    })
    .when("/ad/:id", {
        controller: "singleAdController",
        templateUrl: "Templates/singleAdView.html"
    })
    .when("/myads", {
        controller: "myAdsController",
        templateUrl: "Templates/myAdsView.html"
    })
    .when("/contact", {
        controller: "contactController",
        templateUrl: "Templates/contact.html"
    })
    .otherwise({ redirectTo: "/" });
}]);

// Initializing rootScope data. This run() is called after all modules have been loaded
adAppModule.run(function ($rootScope, dataFactory, imageFactory, categoryFactory, locationFactory) {
    // Initialize search variables which are used in most pages/controllers
    $rootScope.searchData = {
        keyword: null,
        selectedCategoryId: null,
        selectedSubCategoryId: null,
        adminAreaLevel1Id: null,
        adminAreaLevel2Id: null,
        timeout: null
    }

    $rootScope.resetSubCategoryId = function () {
        $rootScope.searchData.selectedSubCategoryId = null;
    }

    $rootScope.resetAdminAreaLevel2Id = function () {
        $rootScope.searchData.adminAreaLevel2Id = null;
    }

    $rootScope.search = function () {
        // Start searching only after the key has been pressed for 500 milliseconds
        if ($rootScope.searchData.timeout) {
            clearTimeout($rootScope.searchData.timeout);
            $rootScope.searchData.timeout = null;
        }
        $rootScope.searchData.timeout = setTimeout(function () {
            // TO DO: need to store searching details in Cache
            dataFactory.getAdsBySearchCriteria($rootScope.searchData.selectedCategoryId, $rootScope.searchData.selectedSubCategoryId, $rootScope.searchData.keyword, $rootScope.searchData.adminAreaLevel1Id, $rootScope.searchData.adminAreaLevel2Id)
            .success(function (result) {
                $rootScope.data = result;
                dataFactory.applyAds(result);
            })
            .error(function (result) {
                alert("Error loading ads!");
            });
        }, 500);
    }

    // User name and role
    $rootScope.userId = $('#userId').val();
    $rootScope.userRole = $('#userRole').val();
        
    getImageUploadFolder($rootScope, imageFactory);

    // Get categories
    getCategories($rootScope, categoryFactory);

    // Get location data
    getLocationData($rootScope, locationFactory);

    // Sort By
    $rootScope.sortBy = "CreatedDate"; // default
});


// Get Image Upload Folder
function getImageUploadFolder($scope, imageFactory) {
    $scope.imageUploadFolder = imageFactory.getInternalImageUploadFolder();
    if ($scope.imageUploadFolder == "") {
        imageFactory.getImageUploadFolder()
        .success(function (result) {
            result = trimQuotes(result);
            $scope.imageUploadFolder = result;
            imageFactory.applyImageUploadFolder(result);
        })
        .error(function (result) {
            alert("Cannot get image upload folder");
        });
    }
}

// getCategories - Function to get categories and sub categories
function getCategories($scope, categoryFactory) {
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

    $scope.subCategories = categoryFactory.getInternalSubCategories();

    if (!$scope.subCategories || $scope.subCategories.length == 0) {
        categoryFactory.getSubCategories()
        .success(function (result) {
            $scope.subCategories = result;
            categoryFactory.applySubCategories(result);
        })
        .error(function (result) {
            alert("Error loading sub-categories!");
        });
    }
}

// getLocationData - Function to get location data
function getLocationData($scope, locationFactory) {
    $scope.countries = locationFactory.getInternalCountries();
    $scope.adminAreaLevel2s = locationFactory.getInternalAdminAreaLevel2s();
    $scope.adminAreaLevel1s = locationFactory.getInternalAdminAreaLevel1s();

    if (!$scope.countries || $scope.countries.length == 0) {
        locationFactory.getCountries()
        .success(function (result) {
            $scope.countries = result;
            locationFactory.applyCountries(result);
        })
        .error(function (result) {
            alert("Error loading countries!");
        });
    }

    if (!$scope.adminAreaLevel2s || $scope.adminAreaLevel2s.length == 0) {
        locationFactory.getAdminAreaLevel2s()
        .success(function (result) {
            $scope.adminAreaLevel2s = result;
            locationFactory.applyAdminAreaLevel2s(result);
        })
        .error(function (result) {
            alert("Error loading adminAreaLevel2s!");
        });
    }

    if (!$scope.adminAreaLevel1s || $scope.adminAreaLevel1s.length == 0) {
        locationFactory.getAdminAreaLevel1s()
        .success(function (result) {
            $scope.adminAreaLevel1s = result;
            locationFactory.applyAdminAreaLevel1s(result);
        })
        .error(function (result) {
            alert("Error loading adminAreaLevel1s!");
        });
    }
}