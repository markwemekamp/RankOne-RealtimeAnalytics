angular.module('umbraco')
    .service('webapiService',
        function ($q, $http, localizationService) {
            this.GetResult = function (url) {
                var deferred = $q.defer();
                $http({ method: 'GET', url: url })
                    .then(function (response) {
                        if (response.data && response.status == 200) {
                            deferred.resolve(response.data);
                        } else {
                            deferred.reject(localizationService
                                .localize("error_page_error", [response.status]));
                        }
                    },
                        function (response) {
                            deferred.reject(response.data.Message);
                        });

                return deferred.promise;
            }
        });