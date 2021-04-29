# BlazorWorld
**Content, Communication, Collaboration, Community, powered by Blazor**

## About

BlazorWorld is a social publishing system built with Blazor. It is a modular platform, with various content modules on the front-end and a common back-end content infrastructure.

It currently has the following modules:
- Articles
- Blogs
- Forums
- Profiles
- Videos

## Demo Site

- Sample site can be found at https://blazorworld.com.
- Features
  - Articles: Features overview https://blazorworld.com/article/blazorworld-features-overview
  - Blog: BlazorWorld's story https://blazorworld.com/blog/post/welcome-to-blazorworld
  - Forums: Awesome Blazor links as forum https://blazorworld.com/forum/general
  - Videos: Carl Franklin's Blazor Train videos https://blazorworld.com/videos/carl-franklins-blazor-train

## Quick Setup

- Go to src/BlazorWorld.Web.Server and update appsettings.json.
- Set the following field values:
  - SiteName: name of your community site
  - Application DB (content data)
    - AppDbProvider: specify if your backend DB is sqlite, mysql, or sqlserver.
    - AppDbFilename: if your DB is sqlite, this is the DB file.
    - ConnectionString/AppDbConnection: connection string for Application DB. Not needed for sqlite.
  - Identity DB (user data) 
    - IdentityDbProvider: just like AppDbProvider, specify if sqlite, mysql, or sqlserver.
    - IdentityDbFilename: For sqlite.
    - ConnectionString/IdentityDbConnection: connection string for Identity DB. Not needed for sqlite.
  - ContactUsEmail: email address used for user emails (email validation, reset password)
  - SendGridKey: key for using SendGrid for user emails

Docker pipeline:
- Build the image using 'docker build -t blazorworld .'
- Start the container 'docker run -p 5000:5000 blazorworld'

## Other Settings

- Go to src/BlazorWorld.Web.Server/Settings for more customization.
- These settings are persisted in the database. It will be updated if the settings have a more recent update date.
- Settings have the following format: Id, Type, Key, Value
- The following settings are available:
  - content-appsettings.json
    - RoleWeight: Sets how much votes a particular role adds
    - PageSize: Sets how many entries are displayed for the particular node type
  - security-appsettings.json
    - RoleUser: For automatically setting the roles for specific usernames
    - Permission: Sets which roles are allowed for specific actions for a node type. Format for the key is 'module:node-type,action'
  - site-appsettings.json
    - SideBarMenu: Sets the sidebar menu items. Format for the value is 'order,,icon,module,visibility'

## Building Your Own Modules

BlazorWorld is a modular system. It is designed to make it easy for developers to add their custom modules.

### Module Structure

- Add your project folder in BlazorWorld.Web.Client.Modules.
- Module project is structured as follows:
  - Models: Content classes, inherited from Node class.
  - Components: Razor files that display specific node types in pages. Services such as NodeService and UserService are injected.
  - Pages: Razor files that work with one or more nodes, each representing an action (Create, Details, Edit)

### Services

#### NodeService

- NodeService provides most of the actions needed to manage your content nodes.
  - GetAsync, GetBySlugAsync, GetCountAsync, GetPageSizeAsync, SecureGetAsync, SecureGetCountAsync: retrieves content or page size
  - AddAsync, UpdateAsync, DeleteAsync: act on your content nodes

#### SecurityService

- SecurityService checks whether the user is allowed to perform a specific action on a particular node type.
- This is set using Permission settings in security-appsettings.json.

#### UserService

- Retrieves user data such as username, user ID, avatar hash (for Gravatar).

### Component Library

BlazorWorld.Web.Client.Shell has a number of components that modules can use for display.

- Avatar: the user's Gravatar
- Created: the node's created date
- Embed: Renders Youtube, Imgur, or Instagram content
- ExpandingTextArea: Editor that increases in size as content is added
- FromNow: relative date/time display
- Loading: animated loader and text
- LocalDateTime
- Modal
- ReadMore: standardized link to detailed content
- RichText: displays embedded social content or Markdown text
- Username
- VoteButtons: up/down arrows (like Reddit)

### Theme

BlazorWorld uses StartBootstrap SB Admin 2 theme. https://github.com/StartBootstrap/startbootstrap-sb-admin-2
