using Entities;

public interface IHotelServices
{
    List<hotel> GetHotels();
    void AddHotel(hotel obj);

    bool UpdateHotel(string id, hotel updatedHotel);
    bool DeleteHotel(string id);
    // add search hotel function...
}

namespace Service
{
    public class HotelServices : IHotelServices
    {
        private List<hotel> _hotels = new List<hotel>();

        public List<hotel> GetHotels()
        {
            return _hotels;
        }

        public void AddHotel(hotel obj)
        {
            _hotels.Add(obj);
        }

        public bool UpdateHotel(string id, hotel updatedHotel)
        {
            int Id = Int32.Parse(id), len = _hotels.Count;
            int idx = len;
            for (int i = 0; i < len; ++i)
            {
                if (_hotels[i].id == Id) idx = i;
            }

            if (idx < len)
            {
                _hotels[idx].name = updatedHotel.name;
                _hotels[idx].city = updatedHotel.city;
                return true;
            }
            return false;
        }

        public bool DeleteHotel(string id)
        {
            int Id = Int32.Parse(id), len = _hotels.Count;
            var idx = _hotels.Count;
            for (int i = 0; i < len; ++i)
            {
                if (_hotels[i].id == Id)
                {
                    idx = i;
                }
            }

            if (idx < len)
            {
                _hotels.RemoveAt(idx);
                return true;
            }
            return false;
        }
    }
}
