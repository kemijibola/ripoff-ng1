'use strict';

/* Controllers */

// Form controller
app.controller('photos', ['photoManager','$scope', function (photoManager,$scope) {


        /* jshint validthis:true */
    $scope.vm = {

        title: 'Upload Image(s)',
        photos: photoManager.photos,
        uploading: false,
        remove:photoManager.remove,
    }

    activate();

    function activate() {
        photoManager.load();
    }

    function setPreviewPhoto(photo) {
        vm.previewPhoto = photo
    }

    function remove(photo) {
        photoManager.remove(photo).then(function () {
            setPreviewPhoto();
        });
    }
       
    }])
;


