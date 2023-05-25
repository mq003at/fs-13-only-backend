using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using store.Models;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role", "unassigned,admin,user");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    images = table.Column<ICollection<string>>(type: "jsonb", nullable: false),
                    creation_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<Role>(type: "role", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    creation_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    images = table.Column<ICollection<string>>(type: "jsonb", nullable: false),
                    creation_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "roles_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_roles_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    creation_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carts", x => x.id);
                    table.ForeignKey(
                        name: "fk_carts_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    cart_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => new { x.cart_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "id", "creation_at", "images", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), new List<string> { "https://picsum.photos/640/640?r=6424" }, "Bracelets", new DateTime(2023, 5, 1, 19, 32, 32, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), new List<string> { "https://picsum.photos/640/640?r=3204" }, "categoria2", new DateTime(2023, 5, 1, 19, 32, 42, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), new List<string> { "https://picsum.photos/640/640?r=6865" }, "categoria3", new DateTime(2023, 5, 1, 19, 32, 54, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), new List<string> { "https://picsum.photos/640/640?r=6761" }, "Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), new List<string> { "https://picsum.photos/640/640?r=212" }, "Others", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2023, 5, 1, 17, 8, 16, 0, DateTimeKind.Utc), new List<string> { "https://placeimg.com/640/480/any" }, "asd", new DateTime(2023, 5, 1, 17, 8, 16, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2023, 5, 1, 19, 21, 11, 0, DateTimeKind.Utc), new List<string> { "https://placeimg.com/640/480/any" }, "New Category", new DateTime(2023, 5, 1, 19, 21, 11, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2023, 5, 1, 19, 23, 15, 0, DateTimeKind.Utc), new List<string> { "https://placeimg.com/640/480/any" }, "New Category", new DateTime(2023, 5, 1, 19, 23, 15, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { 1, "0e49b470-60b6-4c11-aa8a-d0765b9bb3a7", "Admin", "ADMIN" },
                    { 2, "67134645-d0ce-4e33-b828-537b801cd025", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "creation_at", "description", "images", "price", "title", "updated_at" },
                values: new object[,]
                {
                    { 2, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "new description", new List<string> { "https://picsum.photos/640/640?r=6952", "https://picsum.photos/640/640?r=5079", "https://picsum.photos/640/640?r=3071" }, 1000.0, "new title", new DateTime(2023, 5, 1, 21, 33, 51, 0, DateTimeKind.Utc) },
                    { 3, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=5613", "https://picsum.photos/640/640?r=1801", "https://picsum.photos/640/640?r=7886" }, 409.0, "Awesome Cotton Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=7143", "https://picsum.photos/640/640?r=3225", "https://picsum.photos/640/640?r=2958" }, 246.0, "Licensed Frozen Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "A description", new List<string> { "https://placeimg.com/640/480/any" }, 785.0, "sasa", new DateTime(2023, 5, 1, 19, 26, 10, 0, DateTimeKind.Utc) },
                    { 6, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=1231", "https://picsum.photos/640/640?r=6415", "https://picsum.photos/640/640?r=728" }, 285.0, "Handcrafted Steel Gloves", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 7, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=4995", "https://picsum.photos/640/640?r=2645", "https://picsum.photos/640/640?r=8609" }, 353.0, "Incredible Wooden Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 8, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6847", "https://picsum.photos/640/640?r=9608", "https://picsum.photos/640/640?r=9167" }, 954.0, "Practical Wooden Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 9, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=3557", "https://picsum.photos/640/640?r=7453", "https://picsum.photos/640/640?r=166" }, 806.0, "Luxurious Frozen Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 10, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=4291", "https://picsum.photos/640/640?r=8880", "https://picsum.photos/640/640?r=916" }, 197.0, "Intelligent Metal Bacon", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 11, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=9925", "https://picsum.photos/640/640?r=3864", "https://picsum.photos/640/640?r=4230" }, 581.0, "Refined Metal Ball", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 12, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6905", "https://picsum.photos/640/640?r=351", "https://picsum.photos/640/640?r=951" }, 489.0, "Elegant Granite Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 13, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=8092", "https://picsum.photos/640/640?r=9957", "https://picsum.photos/640/640?r=7795" }, 865.0, "Incredible Wooden Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 14, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=2815", "https://picsum.photos/640/640?r=1756", "https://picsum.photos/640/640?r=8966" }, 405.0, "Handmade Concrete Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 15, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6405", "https://picsum.photos/640/640?r=2339", "https://picsum.photos/640/640?r=634" }, 11.0, "Small Metal Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 16, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=7103", "https://picsum.photos/640/640?r=9399", "https://picsum.photos/640/640?r=8927" }, 538.0, "Oriental Bronze Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 17, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=7744", "https://picsum.photos/640/640?r=9955", "https://picsum.photos/640/640?r=4758" }, 937.0, "Generic Steel Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 18, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=1370", "https://picsum.photos/640/640?r=2680", "https://picsum.photos/640/640?r=2172" }, 835.0, "Oriental Cotton Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 19, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=7376", "https://picsum.photos/640/640?r=5584", "https://picsum.photos/640/640?r=5641" }, 609.0, "Recycled Fresh Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 20, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=5400", "https://picsum.photos/640/640?r=6885", "https://picsum.photos/640/640?r=6334" }, 490.0, "Practical Rubber Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 21, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=8197", "https://picsum.photos/640/640?r=8798", "https://picsum.photos/640/640?r=7056" }, 811.0, "Small Wooden Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 22, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=6711", "https://picsum.photos/640/640?r=9280", "https://picsum.photos/640/640?r=4878" }, 498.0, "Incredible Frozen Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 23, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=9670", "https://picsum.photos/640/640?r=3438", "https://picsum.photos/640/640?r=1069" }, 467.0, "Rustic Steel Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 24, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=5693", "https://picsum.photos/640/640?r=6819", "https://picsum.photos/640/640?r=3073" }, 920.0, "Incredible Steel Chicken", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 25, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=4363", "https://picsum.photos/640/640?r=7765", "https://picsum.photos/640/640?r=6346" }, 84.0, "Recycled Cotton Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 26, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=1134", "https://picsum.photos/640/640?r=7374", "https://picsum.photos/640/640?r=4759" }, 339.0, "Modern Cotton Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 27, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=3479", "https://picsum.photos/640/640?r=1259", "https://picsum.photos/640/640?r=8245" }, 767.0, "Gorgeous Concrete Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 28, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=3470", "https://picsum.photos/640/640?r=3378", "https://picsum.photos/640/640?r=717" }, 164.0, "Refined Metal Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 29, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=7969", "https://picsum.photos/640/640?r=4042", "https://picsum.photos/640/640?r=7621" }, 682.0, "Unbranded Concrete Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 30, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=8359", "https://picsum.photos/640/640?r=8846", "https://picsum.photos/640/640?r=850" }, 510.0, "Licensed Frozen Pizza", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 31, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=5641", "https://picsum.photos/640/640?r=4960", "https://picsum.photos/640/640?r=1455" }, 828.0, "Oriental Metal Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 32, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=3805", "https://picsum.photos/640/640?r=7841", "https://picsum.photos/640/640?r=9283" }, 43.0, "Small Metal Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 33, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=7171", "https://picsum.photos/640/640?r=5544", "https://picsum.photos/640/640?r=7608" }, 644.0, "Handcrafted Plastic Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 34, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=7306", "https://picsum.photos/640/640?r=6163", "https://picsum.photos/640/640?r=2883" }, 98.0, "Sleek Plastic Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 35, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=9363", "https://picsum.photos/640/640?r=9580", "https://picsum.photos/640/640?r=1691" }, 847.0, "Ergonomic Granite Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 36, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=8452", "https://picsum.photos/640/640?r=9783", "https://picsum.photos/640/640?r=8500" }, 177.0, "Handcrafted Bronze Chicken", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 37, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=8349", "https://picsum.photos/640/640?r=105", "https://picsum.photos/640/640?r=427" }, 439.0, "Sleek Frozen Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 38, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=3101", "https://picsum.photos/640/640?r=9182", "https://picsum.photos/640/640?r=7052" }, 564.0, "Oriental Soft Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 39, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=3498", "https://picsum.photos/640/640?r=7674", "https://picsum.photos/640/640?r=5804" }, 705.0, "Handcrafted Metal Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 40, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=6908", "https://picsum.photos/640/640?r=252", "https://picsum.photos/640/640?r=4760" }, 597.0, "Modern Wooden Soap", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 41, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=7208", "https://picsum.photos/640/640?r=6147", "https://picsum.photos/640/640?r=4769" }, 285.0, "Gorgeous Wooden Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 42, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=9828", "https://picsum.photos/640/640?r=7777", "https://picsum.photos/640/640?r=9748" }, 447.0, "Awesome Soft Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 43, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=7252", "https://picsum.photos/640/640?r=6575", "https://picsum.photos/640/640?r=3506" }, 739.0, "Awesome Soft Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 44, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=6153", "https://picsum.photos/640/640?r=2345", "https://picsum.photos/640/640?r=2903" }, 308.0, "Unbranded Rubber Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 45, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6864", "https://picsum.photos/640/640?r=6180", "https://picsum.photos/640/640?r=2459" }, 254.0, "Gorgeous Steel Gloves", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 46, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=7085", "https://picsum.photos/640/640?r=7990", "https://picsum.photos/640/640?r=9377" }, 252.0, "Unbranded Steel Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 47, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=4285", "https://picsum.photos/640/640?r=3676", "https://picsum.photos/640/640?r=9172" }, 78.0, "Elegant Concrete Gloves", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 48, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=2209", "https://picsum.photos/640/640?r=8052", "https://picsum.photos/640/640?r=2930" }, 278.0, "Generic Frozen Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 49, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=1309", "https://picsum.photos/640/640?r=692", "https://picsum.photos/640/640?r=5024" }, 379.0, "Modern Fresh Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 50, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=5268", "https://picsum.photos/640/640?r=3603", "https://picsum.photos/640/640?r=3813" }, 98.0, "Refined Frozen Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 51, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=5818", "https://picsum.photos/640/640?r=4942", "https://picsum.photos/640/640?r=3439" }, 673.0, "Ergonomic Rubber Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 52, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=7344", "https://picsum.photos/640/640?r=8454", "https://picsum.photos/640/640?r=2514" }, 450.0, "Recycled Cotton Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 53, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=5855", "https://picsum.photos/640/640?r=2538", "https://picsum.photos/640/640?r=3672" }, 934.0, "Handcrafted Soft Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 54, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=9312", "https://picsum.photos/640/640?r=5091", "https://picsum.photos/640/640?r=4477" }, 435.0, "Fantastic Cotton Gloves", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 55, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=5420", "https://picsum.photos/640/640?r=6842", "https://picsum.photos/640/640?r=594" }, 516.0, "Refined Metal Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 56, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=4030", "https://picsum.photos/640/640?r=4891", "https://picsum.photos/640/640?r=8051" }, 303.0, "Refined Rubber Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 57, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=4084", "https://picsum.photos/640/640?r=7168", "https://picsum.photos/640/640?r=4841" }, 823.0, "Practical Steel Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 58, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=1545", "https://picsum.photos/640/640?r=3508", "https://picsum.photos/640/640?r=3028" }, 166.0, "Practical Metal Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 59, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=1921", "https://picsum.photos/640/640?r=7214", "https://picsum.photos/640/640?r=9529" }, 29.0, "Incredible Frozen Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 60, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=1521", "https://picsum.photos/640/640?r=7463", "https://picsum.photos/640/640?r=2500" }, 305.0, "Sleek Rubber Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 61, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=145", "https://picsum.photos/640/640?r=5966", "https://picsum.photos/640/640?r=4051" }, 51.0, "Unbranded Cotton Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 62, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=4155", "https://picsum.photos/640/640?r=5334", "https://picsum.photos/640/640?r=716" }, 313.0, "Electronic Steel Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 63, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=1741", "https://picsum.photos/640/640?r=9049", "https://picsum.photos/640/640?r=8582" }, 62.0, "Rustic Granite Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 64, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=3946", "https://picsum.photos/640/640?r=3416", "https://picsum.photos/640/640?r=7222" }, 458.0, "Gorgeous Cotton Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 65, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6841", "https://picsum.photos/640/640?r=5997", "https://picsum.photos/640/640?r=7240" }, 797.0, "Handcrafted Plastic Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 66, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=8468", "https://picsum.photos/640/640?r=8415", "https://picsum.photos/640/640?r=9067" }, 429.0, "Oriental Metal Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 67, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=1563", "https://picsum.photos/640/640?r=3316", "https://picsum.photos/640/640?r=2358" }, 779.0, "Generic Frozen Pizza", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 68, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=3199", "https://picsum.photos/640/640?r=3468", "https://picsum.photos/640/640?r=8214" }, 376.0, "Gorgeous Plastic Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 69, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=7705", "https://picsum.photos/640/640?r=285", "https://picsum.photos/640/640?r=5556" }, 757.0, "Handmade Wooden Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 70, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=5617", "https://picsum.photos/640/640?r=2961", "https://picsum.photos/640/640?r=1509" }, 165.0, "Elegant Bronze Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 71, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=1510", "https://picsum.photos/640/640?r=9372", "https://picsum.photos/640/640?r=8910" }, 534.0, "Electronic Bronze Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 72, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=9396", "https://picsum.photos/640/640?r=5126", "https://picsum.photos/640/640?r=211" }, 836.0, "Electronic Frozen Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 73, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=4171", "https://picsum.photos/640/640?r=4756", "https://picsum.photos/640/640?r=3757" }, 204.0, "Incredible Metal Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 74, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=8735", "https://picsum.photos/640/640?r=8690", "https://picsum.photos/640/640?r=1530" }, 213.0, "Oriental Plastic Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 75, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=980", "https://picsum.photos/640/640?r=7410", "https://picsum.photos/640/640?r=8778" }, 437.0, "Tasty Granite Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 76, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=6579", "https://picsum.photos/640/640?r=5674", "https://picsum.photos/640/640?r=3806" }, 277.0, "Oriental Wooden Gloves", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 77, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=7149", "https://picsum.photos/640/640?r=1552", "https://picsum.photos/640/640?r=3481" }, 405.0, "Small Granite Bacon", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 78, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=7742", "https://picsum.photos/640/640?r=9505", "https://picsum.photos/640/640?r=6401" }, 892.0, "Gorgeous Metal Bacon", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 79, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=6742", "https://picsum.photos/640/640?r=8976", "https://picsum.photos/640/640?r=8622" }, 813.0, "Luxurious Steel Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 80, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=194", "https://picsum.photos/640/640?r=8767", "https://picsum.photos/640/640?r=9299" }, 782.0, "Practical Bronze Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 81, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=1115", "https://picsum.photos/640/640?r=6678", "https://picsum.photos/640/640?r=1802" }, 527.0, "Licensed Wooden Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 82, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=4320", "https://picsum.photos/640/640?r=5905", "https://picsum.photos/640/640?r=3734" }, 127.0, "Rustic Cotton Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 83, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=2933", "https://picsum.photos/640/640?r=6846", "https://picsum.photos/640/640?r=2794" }, 132.0, "Electronic Soft Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 84, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=2442", "https://picsum.photos/640/640?r=2197", "https://picsum.photos/640/640?r=7698" }, 557.0, "Tasty Steel Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 85, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=7610", "https://picsum.photos/640/640?r=9747", "https://picsum.photos/640/640?r=9315" }, 588.0, "Unbranded Wooden Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 86, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=2265", "https://picsum.photos/640/640?r=6761", "https://picsum.photos/640/640?r=551" }, 339.0, "Ergonomic Bronze Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 87, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=9095", "https://picsum.photos/640/640?r=4564", "https://picsum.photos/640/640?r=3908" }, 495.0, "Sleek Granite Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 88, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=5202", "https://picsum.photos/640/640?r=2518", "https://picsum.photos/640/640?r=4120" }, 749.0, "Licensed Granite Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 89, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=7370", "https://picsum.photos/640/640?r=7501", "https://picsum.photos/640/640?r=6637" }, 58.0, "Elegant Soft Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 90, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=446", "https://picsum.photos/640/640?r=4697", "https://picsum.photos/640/640?r=5606" }, 134.0, "Awesome Cotton Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 91, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=2022", "https://picsum.photos/640/640?r=7338", "https://picsum.photos/640/640?r=1552" }, 815.0, "Practical Metal Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 92, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=5972", "https://picsum.photos/640/640?r=6782", "https://picsum.photos/640/640?r=6006" }, 627.0, "Handmade Bronze Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 93, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=1816", "https://picsum.photos/640/640?r=8494", "https://picsum.photos/640/640?r=8813" }, 138.0, "Luxurious Cotton Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 94, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=2652", "https://picsum.photos/640/640?r=288", "https://picsum.photos/640/640?r=2615" }, 838.0, "Tasty Frozen Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 95, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=4330", "https://picsum.photos/640/640?r=5706", "https://picsum.photos/640/640?r=7614" }, 816.0, "Gorgeous Bronze Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 96, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=3235", "https://picsum.photos/640/640?r=9560", "https://picsum.photos/640/640?r=4871" }, 867.0, "Rustic Soft Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 97, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=9340", "https://picsum.photos/640/640?r=5214", "https://picsum.photos/640/640?r=4350" }, 534.0, "Rustic Bronze Bacon", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 98, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=8758", "https://picsum.photos/640/640?r=8193", "https://picsum.photos/640/640?r=3285" }, 64.0, "Practical Frozen Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 99, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=3539", "https://picsum.photos/640/640?r=4461", "https://picsum.photos/640/640?r=6337" }, 553.0, "Refined Rubber Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 100, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=7496", "https://picsum.photos/640/640?r=386", "https://picsum.photos/640/640?r=6001" }, 940.0, "Awesome Fresh Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 101, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=430", "https://picsum.photos/640/640?r=314", "https://picsum.photos/640/640?r=4838" }, 653.0, "Sleek Frozen Pizza", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 102, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=9922", "https://picsum.photos/640/640?r=3930", "https://picsum.photos/640/640?r=6417" }, 787.0, "Generic Plastic Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 103, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=806", "https://picsum.photos/640/640?r=6325", "https://picsum.photos/640/640?r=1296" }, 141.0, "Elegant Plastic Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 104, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=3698", "https://picsum.photos/640/640?r=8903", "https://picsum.photos/640/640?r=4673" }, 327.0, "Refined Steel Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 105, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=6230", "https://picsum.photos/640/640?r=8207", "https://picsum.photos/640/640?r=3859" }, 616.0, "Awesome Rubber Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 106, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=3907", "https://picsum.photos/640/640?r=6673", "https://picsum.photos/640/640?r=6707" }, 581.0, "Recycled Wooden Ball", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 107, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=7914", "https://picsum.photos/640/640?r=7907", "https://picsum.photos/640/640?r=3780" }, 360.0, "Practical Concrete Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 108, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=5503", "https://picsum.photos/640/640?r=5601", "https://picsum.photos/640/640?r=9351" }, 502.0, "Handcrafted Plastic Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 109, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=4344", "https://picsum.photos/640/640?r=8561", "https://picsum.photos/640/640?r=4908" }, 117.0, "Practical Concrete Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 110, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=4399", "https://picsum.photos/640/640?r=1208", "https://picsum.photos/640/640?r=6860" }, 556.0, "Electronic Rubber Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 111, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=3225", "https://picsum.photos/640/640?r=4614", "https://picsum.photos/640/640?r=3965" }, 153.0, "Elegant Wooden Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 112, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=3044", "https://picsum.photos/640/640?r=8642", "https://picsum.photos/640/640?r=6108" }, 723.0, "Unbranded Wooden Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 113, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=8430", "https://picsum.photos/640/640?r=6916", "https://picsum.photos/640/640?r=7168" }, 477.0, "Gorgeous Metal Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 115, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=2720", "https://picsum.photos/640/640?r=7088", "https://picsum.photos/640/640?r=2801" }, 727.0, "Incredible Rubber Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 116, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=7920", "https://picsum.photos/640/640?r=224", "https://picsum.photos/640/640?r=4316" }, 19.0, "Recycled Steel Chicken", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 117, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=2108", "https://picsum.photos/640/640?r=7416", "https://picsum.photos/640/640?r=48" }, 476.0, "Elegant Steel Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 118, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=2026", "https://picsum.photos/640/640?r=2728", "https://picsum.photos/640/640?r=6227" }, 480.0, "Practical Metal Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 119, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=101", "https://picsum.photos/640/640?r=4554", "https://picsum.photos/640/640?r=9456" }, 441.0, "Incredible Cotton Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 120, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=7916", "https://picsum.photos/640/640?r=3483", "https://picsum.photos/640/640?r=6321" }, 641.0, "Recycled Frozen Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 121, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=7526", "https://picsum.photos/640/640?r=1238", "https://picsum.photos/640/640?r=2493" }, 904.0, "Elegant Cotton Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 122, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=279", "https://picsum.photos/640/640?r=7741", "https://picsum.photos/640/640?r=4202" }, 1.0, "Unbranded Frozen Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 123, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=3015", "https://picsum.photos/640/640?r=9483", "https://picsum.photos/640/640?r=8267" }, 593.0, "Elegant Soft Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 124, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=5495", "https://picsum.photos/640/640?r=9876", "https://picsum.photos/640/640?r=8859" }, 754.0, "Luxurious Cotton Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 125, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=1868", "https://picsum.photos/640/640?r=4254", "https://picsum.photos/640/640?r=4280" }, 94.0, "Elegant Wooden Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 126, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=5244", "https://picsum.photos/640/640?r=1630", "https://picsum.photos/640/640?r=4972" }, 322.0, "Small Granite Computer", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 127, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=9683", "https://picsum.photos/640/640?r=8134", "https://picsum.photos/640/640?r=8145" }, 8.0, "Handcrafted Concrete Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 128, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=4909", "https://picsum.photos/640/640?r=1174", "https://picsum.photos/640/640?r=9792" }, 105.0, "Rustic Rubber Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 129, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=8259", "https://picsum.photos/640/640?r=9023", "https://picsum.photos/640/640?r=9451" }, 723.0, "Unbranded Metal Chicken", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 130, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=8032", "https://picsum.photos/640/640?r=2744", "https://picsum.photos/640/640?r=8539" }, 219.0, "Recycled Plastic Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 131, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=9321", "https://picsum.photos/640/640?r=5897", "https://picsum.photos/640/640?r=8092" }, 485.0, "Awesome Plastic Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 132, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=2924", "https://picsum.photos/640/640?r=3781", "https://picsum.photos/640/640?r=4418" }, 415.0, "Refined Granite Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 133, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=9392", "https://picsum.photos/640/640?r=7847", "https://picsum.photos/640/640?r=1506" }, 255.0, "Handmade Soft Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 134, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=2680", "https://picsum.photos/640/640?r=386", "https://picsum.photos/640/640?r=2353" }, 492.0, "Handcrafted Cotton Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 135, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=8081", "https://picsum.photos/640/640?r=3186", "https://picsum.photos/640/640?r=7071" }, 840.0, "Luxurious Concrete Pizza", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 136, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=2623", "https://picsum.photos/640/640?r=4127", "https://picsum.photos/640/640?r=7470" }, 498.0, "Awesome Rubber Bacon", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 137, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=4695", "https://picsum.photos/640/640?r=9731", "https://picsum.photos/640/640?r=5301" }, 51.0, "Rustic Concrete Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 138, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=7269", "https://picsum.photos/640/640?r=4353", "https://picsum.photos/640/640?r=2634" }, 123.0, "Generic Frozen Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 139, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=9082", "https://picsum.photos/640/640?r=2157", "https://picsum.photos/640/640?r=5531" }, 557.0, "Modern Wooden Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 140, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=5033", "https://picsum.photos/640/640?r=2254", "https://picsum.photos/640/640?r=3995" }, 418.0, "Electronic Granite Pizza", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 141, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=8318", "https://picsum.photos/640/640?r=6689", "https://picsum.photos/640/640?r=382" }, 113.0, "Electronic Wooden Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 142, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=3281", "https://picsum.photos/640/640?r=9803", "https://picsum.photos/640/640?r=1386" }, 921.0, "Gorgeous Fresh Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 143, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=4293", "https://picsum.photos/640/640?r=1520", "https://picsum.photos/640/640?r=485" }, 995.0, "Oriental Soft Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 144, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=7503", "https://picsum.photos/640/640?r=3992", "https://picsum.photos/640/640?r=1003" }, 890.0, "Rustic Bronze Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 145, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=6225", "https://picsum.photos/640/640?r=1527", "https://picsum.photos/640/640?r=2116" }, 724.0, "Unbranded Rubber Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 146, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=9566", "https://picsum.photos/640/640?r=8011", "https://picsum.photos/640/640?r=2611" }, 517.0, "Ergonomic Frozen Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 147, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=7337", "https://picsum.photos/640/640?r=3593", "https://picsum.photos/640/640?r=1777" }, 193.0, "Generic Bronze Car", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 148, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=8581", "https://picsum.photos/640/640?r=9624", "https://picsum.photos/640/640?r=3522" }, 831.0, "Small Soft Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 149, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=2644", "https://picsum.photos/640/640?r=8659", "https://picsum.photos/640/640?r=80" }, 171.0, "Rustic Frozen Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 150, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=2185", "https://picsum.photos/640/640?r=9799", "https://picsum.photos/640/640?r=4206" }, 399.0, "Fantastic Frozen Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 151, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=216", "https://picsum.photos/640/640?r=4660", "https://picsum.photos/640/640?r=9761" }, 673.0, "Elegant Frozen Soap", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 152, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=1424", "https://picsum.photos/640/640?r=9993", "https://picsum.photos/640/640?r=9345" }, 325.0, "Oriental Steel Ball", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 153, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=548", "https://picsum.photos/640/640?r=718", "https://picsum.photos/640/640?r=3644" }, 523.0, "Licensed Bronze Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 154, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=6664", "https://picsum.photos/640/640?r=8137", "https://picsum.photos/640/640?r=2232" }, 863.0, "Luxurious Concrete Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 155, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=6726", "https://picsum.photos/640/640?r=7596", "https://picsum.photos/640/640?r=5782" }, 15.0, "Handmade Wooden Soap", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 156, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=614", "https://picsum.photos/640/640?r=1471", "https://picsum.photos/640/640?r=7622" }, 873.0, "Unbranded Rubber Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 157, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=7913", "https://picsum.photos/640/640?r=5272", "https://picsum.photos/640/640?r=3383" }, 713.0, "Generic Bronze Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 158, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=334", "https://picsum.photos/640/640?r=9724", "https://picsum.photos/640/640?r=8217" }, 213.0, "Gorgeous Soft Ball", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 159, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=8931", "https://picsum.photos/640/640?r=6573", "https://picsum.photos/640/640?r=9184" }, 838.0, "Bespoke Soft Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 160, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=4758", "https://picsum.photos/640/640?r=607", "https://picsum.photos/640/640?r=9451" }, 578.0, "Recycled Bronze Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 161, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=132", "https://picsum.photos/640/640?r=3545", "https://picsum.photos/640/640?r=645" }, 581.0, "Refined Concrete Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 162, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=1243", "https://picsum.photos/640/640?r=8590", "https://picsum.photos/640/640?r=7214" }, 730.0, "Refined Fresh Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 163, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=9631", "https://picsum.photos/640/640?r=9630", "https://picsum.photos/640/640?r=6401" }, 105.0, "Electronic Fresh Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 164, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=1762", "https://picsum.photos/640/640?r=4648", "https://picsum.photos/640/640?r=7074" }, 172.0, "Tasty Metal Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 165, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=2412", "https://picsum.photos/640/640?r=1541", "https://picsum.photos/640/640?r=7407" }, 81.0, "Handcrafted Steel Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 166, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=1473", "https://picsum.photos/640/640?r=5103", "https://picsum.photos/640/640?r=5679" }, 886.0, "Intelligent Fresh Soap", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 167, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=3758", "https://picsum.photos/640/640?r=2916", "https://picsum.photos/640/640?r=2573" }, 716.0, "Fantastic Rubber Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 168, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=375", "https://picsum.photos/640/640?r=7369", "https://picsum.photos/640/640?r=829" }, 65.0, "Handcrafted Wooden Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 169, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=9811", "https://picsum.photos/640/640?r=9980", "https://picsum.photos/640/640?r=8318" }, 863.0, "Handmade Bronze Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 170, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=3404", "https://picsum.photos/640/640?r=8021", "https://picsum.photos/640/640?r=5947" }, 856.0, "Bespoke Steel Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 171, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=2353", "https://picsum.photos/640/640?r=5276", "https://picsum.photos/640/640?r=6471" }, 275.0, "Gorgeous Plastic Shoes", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 172, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=1337", "https://picsum.photos/640/640?r=3002", "https://picsum.photos/640/640?r=4" }, 485.0, "Fantastic Rubber Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 173, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=4171", "https://picsum.photos/640/640?r=4935", "https://picsum.photos/640/640?r=9910" }, 236.0, "Awesome Steel Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 174, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=519", "https://picsum.photos/640/640?r=1435", "https://picsum.photos/640/640?r=5997" }, 97.0, "Handcrafted Steel Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 175, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=886", "https://picsum.photos/640/640?r=5825", "https://picsum.photos/640/640?r=7130" }, 937.0, "Sleek Wooden Towels", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 176, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=6816", "https://picsum.photos/640/640?r=2181", "https://picsum.photos/640/640?r=6298" }, 859.0, "Rustic Cotton Cheese", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 177, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=2203", "https://picsum.photos/640/640?r=4899", "https://picsum.photos/640/640?r=6700" }, 540.0, "Electronic Soft Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 178, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new List<string> { "https://picsum.photos/640/640?r=2005", "https://picsum.photos/640/640?r=4782", "https://picsum.photos/640/640?r=5464" }, 530.0, "Tasty Concrete Bike", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 179, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=6966", "https://picsum.photos/640/640?r=904", "https://picsum.photos/640/640?r=1280" }, 22.0, "Oriental Bronze Keyboard", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 180, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=7771", "https://picsum.photos/640/640?r=2552", "https://picsum.photos/640/640?r=1088" }, 83.0, "Rustic Cotton Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 181, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=6039", "https://picsum.photos/640/640?r=3235", "https://picsum.photos/640/640?r=924" }, 428.0, "Modern Concrete Chips", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 182, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=6759", "https://picsum.photos/640/640?r=1820", "https://picsum.photos/640/640?r=6791" }, 262.0, "Tasty Cotton Chicken", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 183, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=9927", "https://picsum.photos/640/640?r=5978", "https://picsum.photos/640/640?r=1792" }, 621.0, "Incredible Steel Shirt", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 184, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new List<string> { "https://picsum.photos/640/640?r=7502", "https://picsum.photos/640/640?r=2867", "https://picsum.photos/640/640?r=2570" }, 63.0, "Practical Plastic Tuna", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 185, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new List<string> { "https://picsum.photos/640/640?r=5716", "https://picsum.photos/640/640?r=3914", "https://picsum.photos/640/640?r=8789" }, 817.0, "Practical Fresh Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 186, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Football Is Good For Training And Recreational Purposes", new List<string> { "https://picsum.photos/640/640?r=264", "https://picsum.photos/640/640?r=583", "https://picsum.photos/640/640?r=930" }, 766.0, "Handcrafted Granite Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 187, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=8901", "https://picsum.photos/640/640?r=530", "https://picsum.photos/640/640?r=7670" }, 575.0, "Ergonomic Frozen Table", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 188, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new List<string> { "https://picsum.photos/640/640?r=801", "https://picsum.photos/640/640?r=216", "https://picsum.photos/640/640?r=332" }, 940.0, "Rustic Bronze Pants", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 189, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=3707", "https://picsum.photos/640/640?r=9489", "https://picsum.photos/640/640?r=4327" }, 873.0, "Luxurious Granite Mouse", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 190, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new List<string> { "https://picsum.photos/640/640?r=9436", "https://picsum.photos/640/640?r=1289", "https://picsum.photos/640/640?r=7246" }, 134.0, "Handcrafted Rubber Soap", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 191, 5, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "d", new List<string> { "https://picsum.photos/640/640?r=3074", "https://picsum.photos/640/640?r=4584", "https://picsum.photos/640/640?r=4492" }, 879.0, "Bespoke edfedsRubber Bacon", new DateTime(2023, 5, 1, 19, 41, 1, 0, DateTimeKind.Utc) },
                    { 192, 1, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new List<string> { "https://picsum.photos/640/640?r=880", "https://picsum.photos/640/640?r=3200", "https://picsum.photos/640/640?r=6066" }, 913.0, "Tasty Soft Sausages", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 193, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Boston's most advanced compression wear technologsddvsdy increases muscle oxygenation, stabilizes active muscles", new List<string> { "https://picsum.photos/640/640?r=4749", "https://picsum.photos/640/640?r=953", "https://picsum.photos/640/640?r=3889" }, 256.0, "Electronic Concrete Cheese", new DateTime(2023, 5, 1, 19, 39, 15, 0, DateTimeKind.Utc) },
                    { 195, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new List<string> { "https://picsum.photos/640/640?r=3358", "https://picsum.photos/640/640?r=49", "https://picsum.photos/640/640?r=8019" }, 165.0, "Small Concrete Chair", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 196, 4, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new List<string> { "https://picsum.photos/640/640?r=9688", "https://picsum.photos/640/640?r=5860", "https://picsum.photos/640/640?r=726" }, 503.0, "Luxurious Plastic Fish", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 197, 3, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new List<string> { "https://picsum.photos/640/640?r=5952", "https://picsum.photos/640/640?r=6421", "https://picsum.photos/640/640?r=2627" }, 367.0, "Fantastic Wooden Hat", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 198, 2, new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc), "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new List<string> { "https://picsum.photos/640/640?r=182", "https://picsum.photos/640/640?r=6272", "https://picsum.photos/640/640?r=3166" }, 719.0, "Intelligent Plastic Salad", new DateTime(2023, 5, 1, 16, 1, 55, 0, DateTimeKind.Utc) },
                    { 206, 1, new DateTime(2023, 5, 1, 17, 55, 40, 0, DateTimeKind.Utc), "Wyu wyu dedida warr..,.", new List<string> { "https://api.escuelajs.co/api/v1/files/a8f10.webp" }, 2.0, "BIr marta to'xtadi", new DateTime(2023, 5, 1, 17, 55, 40, 0, DateTimeKind.Utc) },
                    { 208, 1, new DateTime(2023, 5, 1, 18, 36, 47, 0, DateTimeKind.Utc), "What time is?", new List<string> { "https://placeimg.com/640/480/any" }, 1200.0, "Baltazar's book", new DateTime(2023, 5, 1, 19, 31, 50, 0, DateTimeKind.Utc) },
                    { 209, 1, new DateTime(2023, 5, 1, 18, 44, 5, 0, DateTimeKind.Utc), "Allo", new List<string> { "https://placeimg.com/640/480/any" }, 250.0, "New Product Couadsfdsfrse", new DateTime(2023, 5, 1, 19, 31, 26, 0, DateTimeKind.Utc) },
                    { 212, 1, new DateTime(2023, 5, 1, 19, 35, 42, 0, DateTimeKind.Utc), "wew", new List<string> { "https://api.escuelajs.co/api/v1/files/bfe7.jpg" }, 5.0, "WQ", new DateTime(2023, 5, 1, 19, 35, 42, 0, DateTimeKind.Utc) },
                    { 213, 2, new DateTime(2023, 5, 1, 21, 19, 12, 0, DateTimeKind.Utc), "salom", new List<string> { "https://api.escuelajs.co/api/v1/files/178b.png" }, 5.0, "qaw", new DateTime(2023, 5, 1, 21, 19, 12, 0, DateTimeKind.Utc) },
                    { 214, 1, new DateTime(2023, 5, 1, 21, 21, 18, 0, DateTimeKind.Utc), "wersalge", new List<string> { "https://api.escuelajs.co/api/v1/files/bfd4.png" }, 2.0, "sae", new DateTime(2023, 5, 1, 21, 21, 18, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_product_id",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_carts_user_id",
                table: "carts",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_roles_claims_role_id",
                table: "roles_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "roles_claims");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
