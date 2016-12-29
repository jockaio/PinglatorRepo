angular.module('translation', [])
    .controller('translationCtrl', ['$scope', '$http', function ($scope, $http) {
        
        $scope.userInput = "";
        $scope.translation = {};
        $scope.highlighted = "";
        $scope.topThree = [];

        $scope.translate = function() {
            $http.post('/api/WS_Translation/GetTranslation?userInput='+$scope.userInput)
                .success(function (data, status, headers, config) {
                    $scope.translation = data;
                });
        }

        $scope.getTopThreeSearches = function () {
            $http.get('api/WS_Translation/GetTopThreeSearches')
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
    }]);