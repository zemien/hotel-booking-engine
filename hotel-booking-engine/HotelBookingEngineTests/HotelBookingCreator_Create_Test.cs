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

        [Theory]
        [InlineData("50% discount", 100, 0.5, 50)]
        [InlineData("No discount", 100, 0, 100)]
        [InlineData("10% discount", 100, 0.1, 90)]
        [InlineData("100% discount", 100, 1, 0)]
        [InlineData("20.8% discount", 100, 0.28, 72)]
        public void CreateBooking_RequestValid_BookingRoomRate_UsesDiscount(string testName, double publishedRoomRate, double discount, double bookingRoomRate)
        {
            var creator = CreateAvailableBookingCreator();

            var bookingRequestBuilder = new BookingRequestObjectBuilder();
            bookingRequestBuilder.SetDiscount(discount);
            bookingRequestBuilder.SetPublishedRoomRate(publishedRoomRate);

            var result = creator.CreateBooking(bookingRequestBuilder.Build());

            Assert.NotNull(result);
            Assert.NotNull(result.ConfirmedBooking);
            Assert.Equal(bookingRoomRate, result.ConfirmedBooking.BookingRoomRate);
        }
    }
}
