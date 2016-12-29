var translationFactory = function($http) {
    function getTranslation(text) {
        console.log(text);
        return $http.get('api/TranslationObjectModels?text=' + text);
    }

    var service = {
        getTranslation: getTranslation
    };

    return service;
}