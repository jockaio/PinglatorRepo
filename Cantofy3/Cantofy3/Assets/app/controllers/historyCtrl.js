angular.module('history', [])
    .controller('historyCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.getLatestSearches = function () {
            $http.get('api/WS_History/GetTopThreeSearchedWords')
                  .success(function (data, status, headers, config) {
                      console.log(data);
                      $scope.topThree = data;
                  });
        }

    }]);