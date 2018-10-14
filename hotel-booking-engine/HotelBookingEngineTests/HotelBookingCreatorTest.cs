using System;
using Xunit;
using HotelBookingEngine;

namespace HotelBookingEngineTests
{
    public class HotelBookingCreatorTest
    {
        [Fact]
        public void CreateBooking_NullRequest_ErrorResult_NoRequest()
        {
            //Arrange
            var creator = new HotelBookingCreator();

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
            var creator = new HotelBookingCreator();
            var bookingRequest = new BookingRequest();

            //Act
            var result = creator.CreateBooking(bookingRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.ConfirmedBooking);
            Assert.Equal("No hotel", result.ErrorMessage);
        }
    }
}
