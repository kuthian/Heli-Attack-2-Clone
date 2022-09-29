using System;

public enum GunType {
  None,
  Rifle,
  Uzi,
  Shotgun,
}

public enum ConsumableType {
  None,
  Health,
}

static class ItemTypes {

  public static string ToString( GunType type ) 
  {
    // Must match a tag with the same name 
    switch (type) {
      case GunType.Rifle: return "GunRifle";
      case GunType.Uzi: return "GunUzi";
      case GunType.Shotgun: return "GunShotgun";
      case GunType.None: return "GunNone";
      default: return "Error";
    }
  }

  public static GunType GunTypeFromString( string str )
  {
    foreach(GunType type in Enum.GetValues(typeof(GunType)))
    {
      if ( str == ToString(type) ) return type;
    }
    return GunType.None;
  }
}
