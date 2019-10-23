// Database
UserClaims
	|
  Users - UsersRoles - Roles
	|
UsersLogin

// Add roles
Migration -> Configuration -> Seed

// Add custom attributes of user
Models -> IdentityModels -> ApplicationUser

// Set user & password validation
App_Start -> IdentityConfig -> ApplicationUserManager -> Create