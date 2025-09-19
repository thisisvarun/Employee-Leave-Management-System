using backend.Models;
using backend.DTOs;
using backend.Repositories;

namespace backend.Services
{
    public class TeamService(TeamRepository repository)
    {
        private readonly TeamRepository _repository = repository;
        public IEnumerable<TeamReadDTO> GetAllTeams()
        {
            var teams = _repository.GetAll();
            var result = new List<TeamReadDTO>();

            foreach (var team in teams)
            {
                result.Add(new TeamReadDTO
                {
                    Team_Id = team.Team_Id,
                    Team_Name = team.Team_Name,
                    Manager_Id = team.Manager_Id
                });
            }

            return result;
        }

        public TeamReadDTO? GetTeamById(int id)
        {
            var team = _repository.GetById(id);
            if (team == null) return null;

            return new TeamReadDTO
            {
                Team_Id = team.Team_Id,
                Team_Name = team.Team_Name,
                Manager_Id = team.Manager_Id
            };
        }

        public bool CreateTeam(TeamCreateDTO dto)
        {
            var team = new Team
            {
                Team_Name = dto.Team_Name,
                Manager_Id = dto.Manager_Id
            };
            _repository.Add(team);
            return true;
        }

        public bool UpdateTeam(TeamUpdateDTO dto)
        {
            var team = new Team
            {
                Team_Id = dto.Team_Id,
                Team_Name = dto.Team_Name ?? string.Empty,
                Manager_Id = dto.Manager_Id
            };
            return _repository.Update(team);
        }

        public bool DeleteTeam(int id)
        {
            return _repository.Delete(id);
        }
    }
}