using System.Threading.Tasks;
using UnityEngine;

namespace Project.Exersice3.Repositories
{
    public abstract class LocalRepository<Data> : MonoRepository<Data>
    {
        [Header("Debug")]
        [SerializeField] private Data _data;

        public void SaveData(Data data)
        {
            _data = data;
        }

        public override Task<Data> GetData()
        {
            return Task.FromResult(_data);
        }
    }
}