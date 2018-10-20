using System;
using HotelBookingEngine;
using Xunit;
using Moq;

namespace HotelBookingEngineTests
{
    public class HotelBookingCreator_Create_Test
    {
        static BookingRequest CreateValidBookingRequest()
        {
            var hotel = new Hotel();
            var bookingRequest = new BookingRequest(hotel, 5,
                new DateTime(2018, 10, 30), new DateTime(2018, 11, 07),
                new DateTime(2018, 05, 05).ToUniversalTime());
            return bookingRequest;
        }

        [Fact]
        public void CreateBooking_RequestValid_NoErrorMessage()
        {
            var hotelAvailabilityCheckerStub = new Mock<IHotelAvailabilityChecker>();
            hotelAvailabilityCheckerStub.Setup(s => s.IsAvailable(It.IsAny<BookingRequest>()))
                .Returns(true);

            var creator = new HotelBookingCreator(hotelAvailabilityCheckerStub.Object);
            var bookingRequest = CreateValidBookingRequest();

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestValid_ResultRequest_IsTheSame()
        {
            //Note how the Arrange section is exactly the same as previous test
            var hotelAvailabilityCheckerStub = new Mock<IHotelAvailabilityChecker>();
            hotelAvailabilityCheckerStub.Setup(s => s.IsAvailable(It.IsAny<BookingRequest>()))
                .Returns(true);

            var creator = new HotelBookingCreator(hotelAvailabilityCheckerStub.Object);
            var hotel = new Hotel();
            var bookingRequest = CreateValidBookingRequest();

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.NotNull(result.ConfirmedBooking);
            Assert.Equal(bookingRequest, result.ConfirmedBooking.BookingRequest);
        }
    }
}
