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
            var model = new Truck
            {
                Id = 0,
                Model = Enumeration.GetById<TruckModel>(truckModelId),
                ModelYear = DateTime.Now.Year - 1, // error
                ManufacturingYear = DateTime.Now.Year,
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
            var model = new Truck
            {
                Id = 0,
                Model = Enumeration.GetById<TruckModel>(truckModelId),
                ModelYear = DateTime.Now.Year + 2, // error
                ManufacturingYear = DateTime.Now.Year,
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
            [Range(1,9999)] int truckManufaturingYear)
        {
            if (truckManufaturingYear == DateTime.Now.Year) truckManufaturingYear++;
            truckModelYear += DateTime.Now.Year;
            var model = new Truck
            {
                Id = 0,
                Model = Enumeration.GetById<TruckModel>(truckModelId),
                ModelYear = truckModelYear,
                ManufacturingYear = truckManufaturingYear, // error
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
            var model = new Truck
            {
                Id = 0,
                Model = { },
                ModelYear = truckModelYear,
                ManufacturingYear = DateTime.Now.Year,
            };
            var validator = new TruckValidator();
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(truck => truck.Model);
        }
    }
}

// TODO: Study for future use

// Mapping testing
// https://docs.automapper.org/en/stable/Getting-started.html#how-do-i-test-my-mappings

// Test Extensions : Using TestValidate
// https://docs.fluentvalidation.net/en/latest/testing.html
