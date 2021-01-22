using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWorld.Data.Migrations.SqlServer
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

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
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InvitationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "NodeLinks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromNodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ToNodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeLinks", x => x.Id);
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
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NodeCustomFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_NodeCustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeCustomFields_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeTags_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeVersions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeVersions_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeVotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeVotes_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedBy",
                table: "Activities",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_NodeId",
                table: "Activities",
                column: "NodeId");

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
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId_UserId",
                table: "GroupMembers",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email",
                table: "Invitations",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GroupId",
                table: "Messages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField1",
                table: "NodeCustomFields",
                column: "IndexedCustomField1");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField10",
                table: "NodeCustomFields",
                column: "IndexedCustomField10");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField11",
                table: "NodeCustomFields",
                column: "IndexedCustomField11");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField12",
                table: "NodeCustomFields",
                column: "IndexedCustomField12");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField13",
                table: "NodeCustomFields",
                column: "IndexedCustomField13");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField14",
                table: "NodeCustomFields",
                column: "IndexedCustomField14");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField15",
                table: "NodeCustomFields",
                column: "IndexedCustomField15");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField16",
                table: "NodeCustomFields",
                column: "IndexedCustomField16");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField17",
                table: "NodeCustomFields",
                column: "IndexedCustomField17");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField18",
                table: "NodeCustomFields",
                column: "IndexedCustomField18");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField19",
                table: "NodeCustomFields",
                column: "IndexedCustomField19");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField2",
                table: "NodeCustomFields",
                column: "IndexedCustomField2");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField20",
                table: "NodeCustomFields",
                column: "IndexedCustomField20");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField3",
                table: "NodeCustomFields",
                column: "IndexedCustomField3");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField4",
                table: "NodeCustomFields",
                column: "IndexedCustomField4");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField5",
                table: "NodeCustomFields",
                column: "IndexedCustomField5");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField6",
                table: "NodeCustomFields",
                column: "IndexedCustomField6");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField7",
                table: "NodeCustomFields",
                column: "IndexedCustomField7");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField8",
                table: "NodeCustomFields",
                column: "IndexedCustomField8");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_IndexedCustomField9",
                table: "NodeCustomFields",
                column: "IndexedCustomField9");

            migrationBuilder.CreateIndex(
                name: "IX_NodeCustomFields_NodeId",
                table: "NodeCustomFields",
                column: "NodeId",
                unique: true,
                filter: "[NodeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NodeLinks_FromNodeId_Type",
                table: "NodeLinks",
                columns: new[] { "FromNodeId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_NodeLinks_ToNodeId_Type",
                table: "NodeLinks",
                columns: new[] { "ToNodeId", "Type" });

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
                name: "IX_NodeTags_NodeId",
                table: "NodeTags",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeTags_Tag",
                table: "NodeTags",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_NodeVersions_NodeId",
                table: "NodeVersions",
                column: "NodeId");

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
                name: "IX_Reactions_NodeId",
                table: "Reactions",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_NodeId_UserId",
                table: "Reactions",
                columns: new[] { "NodeId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Site_TenantId",
                table: "Site",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "NodeCustomFields");

            migrationBuilder.DropTable(
                name: "NodeLinks");

            migrationBuilder.DropTable(
                name: "NodeTags");

            migrationBuilder.DropTable(
                name: "NodeVersions");

            migrationBuilder.DropTable(
                name: "NodeVotes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Nodes");
        }
    }
}
