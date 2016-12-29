//controllers
var translationController = function ($scope, $http, translationFactory) {

    $scope.translationInput = "";

    $scope.translationObjects = [];

    $scope.error = "";
    $scope.hasErrors = false;

    $scope.translateInput = function () {
        $scope.translationModel = "";
        $scope.error = "";
        $scope.hasErrors = false;

        translationFactory.getTranslation($scope.translationInput).success(function (data) {
            $scope.translationObjects = [];
            console.log("Success");
            data.forEach(function (item) {
                $scope.translationObjects.push(item);
            });
            console.log($scope.translationObjects);
        }).error(function (error) {
            // log errors
            console.log("Error");
            $scope.error = "Not found.";
            $scope.hasErrors = true;
            
        });
    }
    

};

var inputFormController = function () {
    var controller = this;
    controller.models = {
        helloAngular: 'I work!'
    };
}