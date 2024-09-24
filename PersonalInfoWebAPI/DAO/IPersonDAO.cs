using PersonalInfoWebAPI.Models;

namespace PersonalInfoWebAPI.DAO
{
    public interface IPersonDAO
    {
        Task<PersonalInfo> GetPersonalInfoById(int id);
        Task<List<PersonalInfo>> GetPersonalInfos();
        Task<int> InsertPersonalInfo(PersonalInfo personalInfo);
        Task<int> UpdatePersonalInfo(PersonalInfo personalInfo);
        Task<int> DeletePersonalInfo(int id);
    }
}
