// fileInput directive: custom directive to upload multiple files to the server
adAppModule.directive('fileInput', function ($parse) {
    return {
        restrict: "EA",
        template: "<input type='file' multiple />",
        replace: true,
        link: function (scope, el, attrs) {
            var modelGet = $parse(attrs.fileInput);
            var modelSet = modelGet.assign;
            var onChange = $parse(attrs.onChange);
            el.bind('change', function () {
                var files = el[0].files;

                //iterate files since 'multiple' may be specified on the element
                for (var i = 0; i < files.length; i++) {
                    //emit event upward
                    scope.$emit("fileSelected", { file: files[i] });
                }

            });
        }
    };
});
