using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWorld.Data.Migrations.SqlServer
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "EntityCustomFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomField1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomField20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexedCustomField1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField2 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField3 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField4 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField5 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField6 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField7 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField8 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField9 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField10 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField11 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField12 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField13 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField14 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField15 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField16 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField17 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField18 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField19 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IndexedCustomField20 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityCustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badges_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMembers_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvitationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ChildCount = table.Column<int>(type: "int", nullable: false),
                    DescendantCount = table.Column<int>(type: "int", nullable: false),
                    UpVotes = table.Column<int>(type: "int", nullable: false),
                    DownVotes = table.Column<int>(type: "int", nullable: false),
                    Hot = table.Column<double>(type: "float", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeVersions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeVersions_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeVotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<short>(type: "smallint", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeVotes_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFieldsId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_EntityCustomFields_CustomFieldsId",
                        column: x => x.CustomFieldsId,
                        principalTable: "EntityCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BadgeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupMemberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvitationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NodeVersionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NodeVoteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReactionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SettingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SiteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityTags_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_GroupMembers_GroupMemberId",
                        column: x => x.GroupMemberId,
                        principalTable: "GroupMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Invitations_InvitationId",
                        column: x => x.InvitationId,
                        principalTable: "Invitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_NodeVersions_NodeVersionId",
                        column: x => x.NodeVersionId,
                        principalTable: "NodeVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_NodeVotes_NodeVoteId",
                        column: x => x.NodeVoteId,
                        principalTable: "NodeVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Reactions_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "Reactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Settings_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTags_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedBy",
                table: "Activities",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CustomFieldsId",
                table: "Activities",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_NodeId",
                table: "Activities",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_CustomFieldsId",
                table: "Badges",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_UserId",
                table: "Badges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_EntityId",
                table: "EntityCustomFields",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField1",
                table: "EntityCustomFields",
                column: "IndexedCustomField1");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField10",
                table: "EntityCustomFields",
                column: "IndexedCustomField10");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField11",
                table: "EntityCustomFields",
                column: "IndexedCustomField11");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField12",
                table: "EntityCustomFields",
                column: "IndexedCustomField12");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField13",
                table: "EntityCustomFields",
                column: "IndexedCustomField13");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField14",
                table: "EntityCustomFields",
                column: "IndexedCustomField14");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField15",
                table: "EntityCustomFields",
                column: "IndexedCustomField15");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField16",
                table: "EntityCustomFields",
                column: "IndexedCustomField16");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField17",
                table: "EntityCustomFields",
                column: "IndexedCustomField17");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField18",
                table: "EntityCustomFields",
                column: "IndexedCustomField18");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField19",
                table: "EntityCustomFields",
                column: "IndexedCustomField19");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField2",
                table: "EntityCustomFields",
                column: "IndexedCustomField2");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField20",
                table: "EntityCustomFields",
                column: "IndexedCustomField20");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField3",
                table: "EntityCustomFields",
                column: "IndexedCustomField3");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField4",
                table: "EntityCustomFields",
                column: "IndexedCustomField4");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField5",
                table: "EntityCustomFields",
                column: "IndexedCustomField5");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField6",
                table: "EntityCustomFields",
                column: "IndexedCustomField6");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField7",
                table: "EntityCustomFields",
                column: "IndexedCustomField7");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField8",
                table: "EntityCustomFields",
                column: "IndexedCustomField8");

            migrationBuilder.CreateIndex(
                name: "IX_EntityCustomFields_IndexedCustomField9",
                table: "EntityCustomFields",
                column: "IndexedCustomField9");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_ActivityId",
                table: "EntityTags",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_BadgeId",
                table: "EntityTags",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_EntityId",
                table: "EntityTags",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_GroupId",
                table: "EntityTags",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_GroupMemberId",
                table: "EntityTags",
                column: "GroupMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_InvitationId",
                table: "EntityTags",
                column: "InvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_MessageId",
                table: "EntityTags",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_NodeId",
                table: "EntityTags",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_NodeVersionId",
                table: "EntityTags",
                column: "NodeVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_NodeVoteId",
                table: "EntityTags",
                column: "NodeVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_ReactionId",
                table: "EntityTags",
                column: "ReactionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_SettingId",
                table: "EntityTags",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_SiteId",
                table: "EntityTags",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_Tag",
                table: "EntityTags",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_CustomFieldsId",
                table: "GroupMembers",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId_UserId",
                table: "GroupMembers",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CustomFieldsId",
                table: "Groups",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_CustomFieldsId",
                table: "Invitations",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email",
                table: "Invitations",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CustomFieldsId",
                table: "Messages",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GroupId",
                table: "Messages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_CustomFieldsId",
                table: "Nodes",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_GroupId",
                table: "Nodes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_ParentId",
                table: "Nodes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_Slug",
                table: "Nodes",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVersions_CustomFieldsId",
                table: "NodeVersions",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVersions_NodeId",
                table: "NodeVersions",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVotes_CustomFieldsId",
                table: "NodeVotes",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVotes_NodeId",
                table: "NodeVotes",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVotes_NodeId_UserId",
                table: "NodeVotes",
                columns: new[] { "NodeId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ContentId",
                table: "Reactions",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ContentId_UserId",
                table: "Reactions",
                columns: new[] { "ContentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_CustomFieldsId",
                table: "Reactions",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_CustomFieldsId",
                table: "Settings",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_CustomFieldsId",
                table: "Site",
                column: "CustomFieldsId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_TenantId",
                table: "Site",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "EntityTags");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "NodeVersions");

            migrationBuilder.DropTable(
                name: "NodeVotes");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "EntityCustomFields");
        }
    }
}
