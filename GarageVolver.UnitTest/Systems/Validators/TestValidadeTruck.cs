using FluentValidation.TestHelper;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Helpers;
using GarageVolver.Service.Validators;
using GarageVolver.UnitTest.Fixtures;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GarageVolver.UnitTest.Systems.Validators
{
    public class TestValidadeTruck
    {
        [Theory]
        [AutoDomainData]
        public void Should_have_error_when_ModelYear_is_less_than_current_year(
            [Range(0, 1)] int truckModelId)
        {
            var model = new Truck(
                model: Enumeration.GetById<TruckModel>(truckModelId),
                modelYear: DateTime.Now.Year - 1, // error
                manufacturingYear: DateTime.Now.Year
                )
            {
                Id = 0,
            };
            var validator = new TruckValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(truck => truck.ModelYear);
        }

        [Theory]
        [AutoDomainData]
        public void Should_have_error_when_ModelYear_is_more_than_next_year(
            [Range(0, 1)] int truckModelId)
        {
            var model = new Truck(
                model: Enumeration.GetById<TruckModel>(truckModelId),
                modelYear: DateTime.Now.Year + 2, // error
                manufacturingYear: DateTime.Now.Year
                )
            {
                Id = 0,
            };
            var validator = new TruckValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(truck => truck.ModelYear);
        }

        [Theory]
        [AutoDomainData]
        public void Should_have_error_when_ManufacturingYear_is_not_current_year(
            [Range(0, 1)] int truckModelId,
            [Range(0, 1)] int truckModelYear,
            [Range(1, 9999)] int truckManufaturingYear)
        {
            if (truckManufaturingYear == DateTime.Now.Year) truckManufaturingYear++;
            truckModelYear += DateTime.Now.Year;
            var model = new Truck(
                model: Enumeration.GetById<TruckModel>(truckModelId),
                modelYear: truckModelYear,
                manufacturingYear: truckManufaturingYear // error
                )
            {
                Id = 0,
            };
            var validator = new TruckValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(truck => truck.ManufacturingYear);
        }

        [Theory]
        [AutoDomainData]
        public void Should_have_error_when_Model_is_invalid(
            [Range(0, 1)] int truckModelYear)
        {
            truckModelYear += DateTime.Now.Year;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var model = new Truck(
                model: null,
                modelYear: truckModelYear,
                manufacturingYear: DateTime.Now.Year
                )
            {
                Id = 0,
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            var validator = new TruckValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(truck => truck.Model);
        }
    }
}

// Mapping testing
// https://docs.automapper.org/en/stable/Getting-started.html#how-do-i-test-my-mappings

// Test Extensions : Using TestValidate
// https://docs.fluentvalidation.net/en/latest/testing.html
