angular.module('history', [])
    .controller('historyCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.searches = [];

        $scope.getLatestSearches = function () {
            $http.get('api/WS_History/GetLatestSearches')
                  .success(function (data, status, headers, config) {
                      $scope.searches = data;
                  });
        }

        $scope.viewSearch = function (searchID) {
            window.location = '#/home?searchID=' + searchID;
        }

        $scope.getLatestSearches();

    }]);