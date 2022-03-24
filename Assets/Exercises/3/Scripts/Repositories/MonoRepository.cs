using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Exersice3.Repositories
{
    public abstract class MonoRepository<Data> : MonoBehaviour, IRepository<Data>
    {
        public abstract Task<Data> GetData();
    }
}