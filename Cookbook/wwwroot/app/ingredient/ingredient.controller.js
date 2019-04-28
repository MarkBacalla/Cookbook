(function () {
    'use strict';

    angular
        .module('app')
        .controller('Ingredient', Ingredient);

    Ingredient.$inject = ['dataservice'];

    function Ingredient(dataservice) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ingredient';
        vm.newIngredient = '';
        vm.data = dataservice.cache.ingredient;
        

        vm.addIngredient = addIngredient;
        vm.clickIngredient = clickIngredient;
        vm.reset = reset;

        activate();

        function activate() {
        }

        function addIngredient() {
            dataservice.addIngredient(vm.newIngredient)
                .then(dataservice.refreshRecipe)
                .then(function() {
                    vm.newIngredient = '';
                });
        }

        function clickIngredient(ingredient) {
            dataservice
                .updateIngredientAvailability(ingredient.id, null, ingredient.isAvailable)
                .then(dataservice.refreshRecipe);
        }

        function reset() {
            dataservice.resetIngredients()
                .then(dataservice.refreshRecipe);

        }
    }
})();
