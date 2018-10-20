using System;
using Xunit;
using HotelBookingEngine;
using Moq;

namespace HotelBookingEngineTests
{
    public class HotelBookingCreator_Validator_Test
    {
        [Fact]
        public void CreateBooking_NullRequest_ErrorResult_NoRequest()
        {
            //Arrange
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);

            //Act
            var result = creator.CreateBooking(null);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("No request", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithoutHotel_ErrorResult_NoHotel()
        {
            //Arrange
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var bookingRequest = new BookingRequest(null, 2, DateTime.MinValue, DateTime.MaxValue,
                new DateTime(2018, 09, 05).ToUniversalTime());

            //Act
            var result = creator.CreateBooking(bookingRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("No hotel", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithZeroRoomQuantity_ErrorResult_InvalidRoomQuantity()
        {
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 0, DateTime.MinValue, DateTime.MaxValue,
                new DateTime(2018, 09, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("Invalid room quantity", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithNegativeRoomQuantity_ErrorResult_InvalidRoomQuantity()
        {
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, -5, DateTime.MinValue, DateTime.MaxValue,
                new DateTime(2018, 09, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("Invalid room quantity", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithCheckInDateGreaterThanCheckOutDate_ErrorResult_InvalidDates()
        {
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 09, 05),
                new DateTime(2018, 09, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("Invalid dates", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithCheckInDateEqualToCheckOutDate_ErrorResult_InvalidDates()
        {
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 10, 30),
                new DateTime(2018, 09, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("Invalid dates", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestWithCheckInDateBeforeCreated_ErrorResult_InvalidDates()
        {
            var creator = new HotelBookingCreator(new Mock<IHotelAvailabilityChecker>().Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 11, 07),
                new DateTime(2018, 11, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("Invalid dates", result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestNoAvailability_ErrorResult_NoAvailability()
        {
            var hotelAvailabilityCheckerStub = new Mock<IHotelAvailabilityChecker>();
            hotelAvailabilityCheckerStub.Setup(s => s.IsAvailable(It.IsAny<BookingRequest>()))
                .Returns(false);

            var creator = new HotelBookingCreator(hotelAvailabilityCheckerStub.Object);
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 11, 07),
                new DateTime(2018, 05, 05).ToUniversalTime());

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("No availability", result.ErrorMessage);
        }
    }
}