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
`object CanRemoveAdmin(BasePlayer Player)`
`object CanAddAdmin(BasePlayer Player)`
`void OnAdminAdded(BasePlayer Player)`
`void OnAdminRemoved(BasePlayer Player)`

Example:

```csharp
private const permission_addadmin = "example.addadmin";

object CanAddAdmin(BasePlayer Player) {
  if(!permission.UserHasPermission(Player.UserIDString, permission_addadmin)) return false;
  return null;
}
```
