# TomasosPizza
ASP .NET Core app with Core identity


Skall bestå av en enhetlig layout som används i alla vyer och en gemensam navigering för
att navigera mellan olika sidor. Ni skall använda CSS för formateringar av färg, typsnitt,
fonter och liknande. För detta måste Bootstrap användas i så stor utsträckning som möjligt.

- Det skall finnas användare med 3 olika behörighetsroller, Admin, PremiumUser samt
RegularUser. Ni måste använda Core Identity för detta.

- När en admin loggar in skall den få tillgång till hela sajten och kunna komma åt
admingränssnittet. Där skall det gå att lägga till/uppdatera pizzor/maträtter och
ingredienser, ta bort ordrar eller ändra status på en order. Det skall även gå att uppdatera
en RegularUser till PremiumUser eller tvärtom.

- Vanliga användare kan registrera sig och får då rollen RegularUser. Denna roll skall fungera
precis som i uppgift 2. Sedan kan en admin ändra denna roll till PremiumUser. Att en
användare är PremiumUser innebär att den har vissa rabatter. Köper den tre
pizzor/maträtter eller mer får den 20% rabatt på beställningen. För varje pizza eller
maträtt den köper får den 10 bonuspoäng. När den har 100 poäng ger det en gratis pizza
vid en beställning.

- Det skall inte gå att öppna eller komma åt någon del av applikationen om användaren inte
är inloggad. Applikationen skall känna av vilken typ av användare som är inloggad och
anpassas efter det.

- Validering måste finnas på alla fält där det behövs när värden matas in. Du måste använda
AJAX anrop i applikationen för åtminstone en vy.

- Entity framework skall användas för modellen. Koden i C# skall vara snyggt strukturerad
och innehålla kommentarer om något behöver förtydligas. 

