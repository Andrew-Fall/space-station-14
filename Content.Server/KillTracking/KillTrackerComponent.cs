using Content.Shared.FixedPoint;
using Content.Shared.Mobs;
using Robust.Shared.Network;

namespace Content.Server.KillTracking;

/// <summary>
/// This is used for entities that track player damage sources and killers.
/// </summary>
[RegisterComponent, Access(typeof(KillTrackingSystem))]
public sealed partial class KillTrackerComponent : Component
{
    /// <summary>
    /// The mobstate that registers as a "kill"
    /// </summary>
    [DataField("killState")]
    public MobState KillState = MobState.Critical;

    /// <summary>
    /// A dictionary of sources and how much damage they've done to this entity over time.
    /// </summary>
    public Dictionary<KillSource, FixedPoint2> LifetimeDamage = new();
}

public abstract record KillSource;

/// <summary>
/// A kill source for players
/// </summary>
public sealed record KillPlayerSource : KillSource
{
    public NetUserId PlayerId;

    public KillPlayerSource(NetUserId playerId)
    {
        PlayerId = playerId;
    }
}

/// <summary>
/// A kill source for non-player controlled entities
/// </summary>
public sealed record KillNpcSource : KillSource
{
    public EntityUid NpcEnt;

    public KillNpcSource(EntityUid npcEnt)
    {
        NpcEnt = npcEnt;
    }
}

/// <summary>
/// A kill source for kills with no damage origin
/// </summary>
public sealed record KillEnvironmentSource : KillSource;
