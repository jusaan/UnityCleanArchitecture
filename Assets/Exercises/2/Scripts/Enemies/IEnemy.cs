using Project.Exersice3.Repositories;

namespace DefaultNamespace
{
    public interface IEnemy
    {
        void Walk();
        void Run();
        void Attack();
        void RegenerateLife();
        void Die();
    }

    public interface NPC
    {
        void Walk();
        void Run();
    }

    public interface Boss
    {
        void RegenerateLife();
        void Die();
    }
}