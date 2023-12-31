using BehaviourTree;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AI;

public class AllyBT : Tree
{
    [UnityEngine.SerializeField]
    private AllySettings _allySettings;

    public static AllySettings Settings;
    public UnityEngine.Transform PlayerTransform;
    public NavMeshAgent Nav;

    public static float speed(float dist)
    {
        float proportionalDistance = (dist - Settings.StopDist) / (Settings.SlowDist - Settings.StopDist);
        float speed = UnityEngine.Mathf.Lerp(0, Settings.MaxSpeed, proportionalDistance);

        return speed;
    }
    /// <summary>
    /// korte Nl uitleg zodat ik het zelf op een rijtje heb:
    /// er wordt een root aangemaakt en vanaf daar beginnen we met een selector. binnen deze selector kan je zoveel sequences aanmaken als je wilt, 
    /// hij blijft ze toch allemaal checken totdat hij een succes vind. 
    /// de hierarchie maakt uit, want als er twee sequences zijn die succes returnen, pakt hij alleen de eerste die hij vind.
    /// de sequence runt dan iedere keer zn eerste child omdat die een failure returnt.
    /// totdat hij een succes returnt en als dat zo is dan begint de task (de leaf als je het zo wilt noemen, helpt wel bij visualisatie in je hoofd)
    /// 
    /// </summary>
    /// <returns></returns>
    protected override Node SetupTree()
    {
        Node Root = new Selector(new List<Node>{
            new Sequence(new List<Node>{
                new CheckHidingSpotReached(transform),
                new TaskThrowProjectile(transform),
            }),
            new Sequence(new List<Node>{
                new CheckEnemyNear(transform, Settings.EnemyMask),
                new CheckForNearbyTree(transform),
                new TaskHideFromEnemy(transform,Nav),
            }),
            new TaskFollowPlayer(transform, PlayerTransform, Nav),
        });
        return Root;
    }
    protected override void Initialization()
    {
        if (_allySettings == null)
        {
            UnityEngine.Debug.LogWarning("AllySettings not assigned in AllyBt.cs");
        }
        else
        {
            Settings = _allySettings;
        }
        Nav = GetComponent<NavMeshAgent>();
        if (Nav == null)
        {
            UnityEngine.Debug.LogWarning("no navmesh on the AllyAI");
        }
        Nav.speed = Settings.MaxSpeed;
        Nav.stoppingDistance = Settings.SlowDist;
    }
}
