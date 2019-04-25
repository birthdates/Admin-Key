# Admin-Key

Get admin from a key code instead of going into console

## Configuration

```json
{
  "Admin keys": ["abc123", "123abc"]
}
```

## Developers

The developer API has been added:

```csharp
object CanRemoveAdmin(BasePlayer Player)
```

```csharp
object CanAddAdmin(BasePlayer Player)
```

```csharp
void OnAdminAdded(BasePlayer Player)
```

```csharp
void OnAdminRemoved(BasePlayer Player)
```

Example:

```csharp
private const string permission_addadmin = "example.addadmin";

object CanAddAdmin(BasePlayer Player) {
  return permission.UserHasPermission(Player.UserIDString, permission_addadmin);
}
```
