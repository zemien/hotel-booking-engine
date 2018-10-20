using System;
using HotelBookingEngine;
using Xunit;
using Moq;

namespace HotelBookingEngineTests
{
    public class HotelBookingCreator_Create_Test
    {
        private static HotelBookingCreator CreateAvailableBookingCreator()
        {
            var hotelAvailabilityCheckerStub = new Mock<IHotelAvailabilityChecker>();
            hotelAvailabilityCheckerStub.Setup(s => s.IsAvailable(It.IsAny<BookingRequest>()))
                .Returns(true);

            var creator = new HotelBookingCreator(hotelAvailabilityCheckerStub.Object);
            return creator;
        }

        [Fact]
        public void CreateBooking_RequestValid_NoErrorMessage()
        {
            var creator = CreateAvailableBookingCreator();
            var bookingRequest = BookingRequestObjectMother.CreateValidBookingRequest();

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void CreateBooking_RequestValid_ResultRequest_IsTheSame()
        {
            var creator = CreateAvailableBookingCreator();
            var bookingRequest = BookingRequestObjectMother.CreateValidBookingRequest();

            var result = creator.CreateBooking(bookingRequest);

            Assert.NotNull(result);
            Assert.NotNull(result.ConfirmedBooking);
            Assert.Equal(bookingRequest, result.ConfirmedBooking.BookingRequest);
        }

        [Fact]
        public void CreateBooking_RequestValid_ResultHotel_IsTheSameName()
        {
            var creator = CreateAvailableBookingCreator();

            var bookingRequestBuilder = new BookingRequestObjectBuilder();
            bookingRequestBuilder.SetHotelName("Mercure Hotels Taupo");

            var result = creator.CreateBooking(bookingRequestBuilder.Build());

            Assert.NotNull(result);
            Assert.NotNull(result.ConfirmedBooking);
            Assert.NotNull(result.ConfirmedBooking.Hotel);
            Assert.Equal("Mercure Hotels Taupo", result.ConfirmedBooking.Hotel.Name);
        }

        [Fact]
        public void CreateBooking_RequestValid_BookingRoomRate_UsesDiscount()
        {
            var creator = CreateAvailableBookingCreator();

            var bookingRequestBuilder = new BookingRequestObjectBuilder();
            bookingRequestBuilder.SetDiscount(0.5);
            bookingRequestBuilder.SetPublishedRoomRate(100);

            var result = creator.CreateBooking(bookingRequestBuilder.Build());

            Assert.NotNull(result);
            Assert.NotNull(result.ConfirmedBooking);
            Assert.Equal(50, result.ConfirmedBooking.BookingRoomRate);
        }
    }
}
