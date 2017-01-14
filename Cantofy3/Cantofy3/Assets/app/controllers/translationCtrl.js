angular.module('translation', [])
    .controller('translationCtrl', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {
        
        $scope.searchID = $routeParams.searchID;
        $scope.userInput = "";
        $scope.translation = {};
        $scope.highlighted = "";
        $scope.topThree = [];

        $scope.translate = function() {
            $http.post('/api/WS_Translation/GetTranslation?userInput=' + $scope.userInput + '&searchID=0')
                .success(function (data, status, headers, config) {
                    $scope.translation = data;
                });
        }

        $scope.translateHistoricSearch = function (searchID) {
            $http.post('/api/WS_Translation/GetTranslation?userInput=&searchID=' + searchID)
                .success(function (data, status, headers, config) {
                    $scope.translation = data;
                });
        }

        $scope.getTopThreeSearches = function () {
            $http.get('api/WS_History/GetTopThreeSearchedWords')
                  .success(function (data, status, headers, config) {
                      console.log(data);
                      $scope.topThree = data;
                  });
        }

        $scope.highlight = function (item) {
            $scope.highlighted = item;
            console.log("clicked" + item);
        }

        $scope.getTopThreeSearches();

        if ($scope.searchID != null) {
            $scope.translateHistoricSearch($scope.searchID);
        }
    }]);