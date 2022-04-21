using MediatR;
using Signalr_poc.Repository;

namespace Signalr_poc.Feature.Rooms.Query.GetAllRooms
{
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, GetAllRoomsQueryResult>
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<GetAllRoomsQueryResult> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = _roomRepository.GetAllRooms().ToList();
            return Task.FromResult(new GetAllRoomsQueryResult() { Rooms = rooms });
        }
    }
}
