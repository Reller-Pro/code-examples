# Angular Best Practices

# Description

This guide file provide a set of `Angular` codestyle rules and architecture patterns for any project.
The goal is to describe the minimum of best practices for angular.
This guide is designed for beginners in Angular so that new programmers can write quality code.

Following these guidelines will save a team lead from explaining these basics, making it easier to switch personnel.
Ultimately, adhering to the principle that "putting in effort leads to better outcomes" becomes necessary.
Suggestions for improving this guide are appreciated.

Before starting learning the project, remember these things:

1. There are "unimportant things" - these are development approaches where refactoring doesn't take much time. Examples include issues with naming folders, components, services, and so on. It's not crucial which specific names are used, but more important that the same approach is taken throughout the whole project.
2. There are "important things" - these are development approaches where refactoring takes significant time, and it's often easier to rewrite logic than to refactor it. Examples include issues with architecture and the proper use of libraries and frameworks.

Thus, feel free to approach "unimportant things" as desired, but for "important things," it's recommended to follow this guide.

## Contents

- [Files](#files)
- [Project structure](#project-structure)
- [Interfaces](#interfaces)
- [Formatting](#formatting)
- [Packages](#packages)
- [Lint](#lint)
- [Git](#git)
- [TypeScript](#typescript)
- [JavaScript](#javascript)
- [Angular](#angular)
    - [Mocking](#mocking)
        - [Mocking via service](#mocking-via-service)
        - [Mocking via backend](#mocking-via-backend)
    - [Template](#template)
- [RxJs](#rxjs)
- [Architecture](#architecture)
- [Comments](#comments)
- [Css styles](#css-styles)
- [State manager](#state-manager)
    - [NGXS](#ngxs)
- [Forms](#forms)
    - [Formly](#formly)

## Files

It's best to store each class, interface, enum, and record in separate files.
Each file should only contain one of these entities.

If the `interface` is describing methods, the file should end with `***.interface.ts`, (e.g. `example.interface.ts`, and name it `ISimpleInterface`).

If the `interface` is describing a value object, the file should end with `***.ts` (e.g. `example.ts`).

If it's a `dictionary (record)`, the file should end with `***.ts` (e.g. `example.ts`).

If it's an `enum`, the file should end with `***.ts` (e.g. `example.ts`).

And finally, if it's a `class`, the file should end with `***.ts` (e.g. `example.ts`).

## Project structure

Split the project logic into the modules.

1. App Module:
   The App Module is the root module of your application, it imports other modules and declares components, services, guards, pipes and directives. This structure should follow the single responsibility principle where one module should handle only one primary responsibility.
2. Shared Module:
   The Shared Module contains interceptors, guards, services, directives, components, helpers, and pipes which will be used application-wide. Reusable components like Header, Footer, and Breadcrumb should be added in this module.
3. Routing Module:
   Every Angular application must have a routing module, which provides a mapping between URLs and your application views.
4. Services:
   Services provide a way to share data, logic, and functionality across different parts of your application.
5. Components:
   Components are the building blocks of any Angular application. Divide the application into smaller reusable components.
6. Pipes:
   Pipes can be used to perform a transformation of any kind of data that is displayed in an Angular template.
7. Directives:
   Directives are reusable behaviors that can be added to an element in your Angular template.
8. Guards:
   Guards allow us to execute a specific code based on certain conditions, such as whether a user is logged in.
9. Models:
   Models help to maintain a multimodule application structure in Angular.
10. Resolvers:
    Resolver is a service that is used to fetch some data before the component is loaded. It is used to ensure that the data is available before the user sees the view. Resolvers are useful when working with asynchronous data such as making HTTP requests or fetching data from a database.
11. Providers:
    Provider is a type of service that provides objects or values that can be injected into components or other services. Providers serve as a bridge between the dependency injection system and the components or services that need those dependencies.
12. Constants:
    Constants are values that cannot be changed once they have been defined.

Example:

```
-app
    -modules
        -account
            -components
                -list
                    account-list.component.html
                    account-list.component.less
                    account-list.component.ts
                    account-list.component.spec.ts
            -models
                account.model.ts
            -services
                -facade
                    account-facade.service.ts
                -api
                    account-api.service.ts
            -resolvers
                account.resolver.ts
            -providers
                account-service.provider.ts
            -pipes
                account.pipe.ts
            -guards
                account.guard.ts
            -store // Redux ( ngxs example )
                -actions // the models of actions
                -models // the models of states and theris default values
                -states //  the actions effects and their selectors
            -constants
                account.constant.ts
            
            account.component.ts // this component should act like a container/smart component
            account.component.html
            account.module.ts
            account.route.ts
            
            customer-info // nested module
        -....
    -shared
        -components
        -directives
        -pipes
        -services
        -interceptors
        -guards
        -models
        -helpers
        
    app.component.ts
    app.component.html
    app.component.spec.ts
    app.module.ts
    app-routing.module.ts
```

Give names to nested modules following the business logic.

Example:

`customer-info`
`details`

## Interfaces

If the interface is used in one component, it can be declared inside that component, 
but if it's used in multiple components, it should be extracted into a shared module.

## Formatting

To make your code look better, use `prettier` for formatting. 

Create a file called `.prettierrc.json` in the `./src` directory and specify your preferred formatting settings there. 
Your IDE may prompt you to apply these settings, so do it.
Create a file called .`prettierignore` in the `./src` directory and input the file paths that you do not want to be formatted.
Finally, configure your `IDE` to automatically format the code when you save the document.

## Packages

To avoid updating package versions, you should not include any version information before the package name in `package.json`.
For example, `"moment": "2.24.0"`. Refer to this [table](https://stackoverflow.com/a/25861938/7160632) for more information.

You can set the default behavior in `npm` by following these steps:

```
1. Create an .npmrc file in the project root folder.
2. Add "save-exact=true" to the .npmrc file.
```

## Lint

`Tslint` and `Eslint` can help you detect errors in your project's code. 
Make sure to use the version that works with `Angular`. 
You can incorporate code check rules into your project by installing third-party packages such as `rxjs-tslint-rules`.

### Tslint

To improve code quality, add code check packages to `tslint.json` like `"extends": ["tslint:recommended", "rxjs-tslint-rules"]`.
Then specify which rules to use by adding them to the "rules" property, like `"rxjs-no-unsafe-takeuntil": { "severity": "error" }`.

To run the code check, use the command `ng lint --format prose` and add it to `package.json`.

## Git

### CRLF LF

If you get a warning from Git after using the `git add .` 
command, you can fix it by adding the rule `* text=auto eol=lf` to the `.gitattributes` file.

## TypeScript

To make your code more consistent and reliable, it is recommended to use `"strict": true`. 
This means that you need to specify the access modifiers for every member and method of your classes, such as `private`, `public`, or `protected`. 
You also need to define the data types for each property, constant, function argument, and return value of a function.
Avoid using optional settings like `?` (e.g. `private a?: number;`), as your code should be as strict as possible.

In addition, it is recommended to avoid using the `any` type, except when working with third-party libraries and there's no other alternative. 
Use `generics` when you need to use any types inside your model, or use type `union` when dealing with multiple types. 
This will make your code more consistent and reliable.

Bad:

```TypeScript
interface DraftDTO {
    id: number;
    body: any
}

const contentBody: any = { type: "content" }
const draftContent: DraftDTO = { id: 1, body: contentBody }
```

Good:

```TypeScript
interface DraftDTO<T> {
    id: number;
    body: T
}

const contentBody: ContentDTO = { type: "content" }
const draftContent: DraftDTO<ContentDTO> = { id: 1, body: contentBody }

type CustomDraftBody = ContentDTO | SoftDTO;

const softBody: CustomDraftBody = { type: "soft" }
const draftCustom: DraftDTO<CustomDraftBody> = { id: 1, body: softBody }
```

Avoid objects' mutations. 
To prevent this, make sure all properties in classes and interfaces cannot be changed by adding a `readonly` modification. 
This will help prevent changes from happening between parent and child components. 
It also makes it easier when using `OnPush` strategies because you will need to make copies of objects and change their references when needed. 
If you can't set the properties to `readonly`, add `Readonly<X>` to the objects.

Bad:

```TypeScript
userDTO: IUserDTO[] = [];
const user = Readonly<User>
```

Good:

```TypeScript
userDTO: Readonly<IUserDTO>[] = [];
const user = Readonly<User>
```

Always use `const` or `let` when you need to change reference. Avoid use `var`.
Always use strict comparison `===`.
If you have a value that won't be initialized in the constructor, such as from `BehaviourSubject`, 
turn on strict mode in TypeScript to catch errors. Use the symbol `!` to mark it as not `undefined` or `null`.

Example:

```TypeScript
@Input() addressee: string | undefined = undefined;

public mobileInfo!: MobileInfo

ngOnInit(){
   this.mobileInfo.pipe(takeUntil(this.destroy$)).subscribe(mobileInfo => this.mobileInfo = mobileInfo)
}
```

Avoid calling objects by using strings, because it breaks the `DRY` principle.
Don't use routes as strings, use `enum` to store and access your routes.

Bad:

```TypeScript
// controller
this.router.navigate(['info'])

[routerLink]="/history"
```

Good:

```TypeScript
// controller
this.router.navigate([Routes.USERS])

[routerLink]=['/' + Routes.HISTORY]
```

Use nouns. Do not give names to entities beginning with verbs.

Bad:

```TypeScript
export interface CreateUserInfoModel {
    id: number;
    name: string;
    }
```

Good:

```TypeScript
export interface UserInfoCreateModel {
    id: number;
    name: string;
    }
```

When you need to access the values of an object, it's better to use a type-safe iterator rather than manually declaring it.

Bad:

```TypeScript
const startStatus: StoreStatus | undefined = [
    state.productsList.status,
    state.productsComposition.status,
    state.offerStatesList.status,
    state.offerStoreList.status,
    state.offerUserList.status,
].find((currSatus: StoreStatus) => currSatus === StoreStatus.START);
```

Good:

```TypeScript
const loadingStatus: StoreStatus | undefined = (Object.keys(state) as (keyof ProductsOffersStateModel)[])
    .map((key) => state[key])
    .map((value) => value.status)
    .find((currSatus: StoreStatus) => currSatus === StoreStatus.START);
```

If any external libraries allow the usage of generics, it's better to use them. 
For example, `Angular Material` dialog uses generics, because type checks helps you to support and extend project code much easier.

Example:

```TypeScript
export class DialogComponent implements OnInit {
    public dialogModel: DialogModel;

    constructor(
        private dialogRef: MatDialogRef<DialogComponent, DilogResultModel>
    )

    public ngOnInit() {
    ...
    }

    public openDilog(data: DialogModel): Observable<DilogResultModel | undefined> {
        return this.dialog
            .open<DialogComponent, DialogModel, DilogResultModel>(DialogComponent, {
                data,
                disableClose: data.isDisableCloseOnBackClick,
            })
            .afterClosed();
    }
}
```

If any external libraries allow the use of `Observables`, it's better to use them inside `guards` or on button clicks inside components. 
Angular Material dialog, for example, uses `Observables` after the `.afterClosed()` method.

Example:

```TypeScript
switchMap((isInitialized) => {
    if (isInitialized) {
        return this.dialogService.getUserData(this.dialogModel).pipe(
            map((result) => !result?.userData)
        );
    }

    return of(true);
})
```

## JavaScript

Don't use `side effects`.

If a function with an argument changes something, then we start calling it with the word `set`.

If a function without an argument changes something, then we call it from the word `change`.

If a function retrieves a value, we use the word `get`, whether there are arguments or not.

Bad:

```TypeScript
userData() : string
doIt(data: SomeType) : string
```

Good:

```TypeScript
getSomething() : string
setSomething(data: SomeType) : string
changeSomething() : void
```

Try to use array functions `filter`,` map`, `reduce`,` every`, `some`, `includes`, `splice` more often.

If you need to check a certain field in an array and perform an action only if it is true, you can use the `find` function. 
This makes the code easier to read and understand. 
For example, instead of using `forEach` and `includes` to make changes to an array, you can use `map` and `filter` instead.

Bad:

```TypeScript
this.userIteration = Object.keys(userNames)
.map((key) => new UserIteration(parseInt(key, 10), userNames[key as number])
);
```

Good:

```TypeScript
this.userIteration = Object.keys(userNames)
    .map((keyAsString) => parseInt(keyAsString, 10))
    .map((key) => new UserIteration(key, userNames[key]));
```

When writing arrow functions, it's important to name your arguments logically. 
Using names like `countersDTO` and `maxDate` makes it clear what they contain. 
Avoid using unreadable abbreviations such as `c` and `m`.

Bad:

`if(arr.find( u => u.userName === 'Gale' ))`

Good:

`if(users.find( user: User => user.userName === 'Gale' ))`

All boolean values should be start with `is` or `has`.

Bad:

```TypeScript
showDilog: boolean
```

Good:

```TypeScript
isShowDilog: boolean
```

Avoid using more than one ternary operator `?` at once. Instead, check the condition inside an `if() {return} return`.

Bad:

```TypeScript
return link.includes('range1')
      ? ITEM1.GROUPS
      : link.includes('range2')
      ? ITEM2.DOMAINS
      : link.includes('range3')
      ? ITEM3.BUSINESS_CAPABILITIES
      : ITEM4.GROUPS;
```

Good:

```TypeScript
 if(link.includes('range1'))
    return ITEM1.GROUPS
 if(link.includes('range2'))
    return ITEM2.DOMAINS
 if(link.includes('range3'))
    return ITEM3.BUSINESS_CAPABILITIES
 return ITEM4.GROUPS
```

Avoid using expressions longer than 70 characters in arrays, object values, and if() statements. 
Instead, move them into separate constants or functions. 
By assigning expressions to constants, we enhance code understanding, readability, and ease of debugging.

Bad:

```TypeScript
if (
    (typeof data.currValue && typeof data.previousValue) === 'string' &&
    (data.currValue ? data.currValue : '').length + data.previousValue.length > 0 &&
    data.key !== 'owner' &&
    data.key !== 'creationDate'
)
```

Good:

```TypeScript
const isValueIsString: boolean = (typeof data.currValue && typeof data.previousValue) === 'string';
const currValue: number = data.currValue ? data.currValue.length : 0
const isLengthNotZero: number = currValue + data.previousValue.length > 0
const isKeyNotExist: boolean =
    data.key !== 'owner' &&
    data.key !== 'creationDate';

if (isValueIsString && isLengthNotZero && isKeyNotExist)
```

Instead of using a `Record`, we prefer to use the interface as an associative array. 
This is because you can specify property names, which enhances the code's readability.

Bad:

```TypeScript
sectionOffers: Record<string, SectionOffers>;
```

Good:

```TypeScript
sectionOffers: { [valueId: string]: SectionOffers }
```

Within the method, you should use only one level of curly braces for nesting. 
Otherwise, consider creating an alternate option using `if () {}` with just one level of nesting, or dividing the method into several smaller methods.

Bad:

```TypeScript
function getUserData() {
    if (currentUser) {
        if (currentUser.data) {
            return currentUser.data;
        }
    }

    return null;
}
```

Good:

```TypeScript
function getUserData() {
    if (!currentUser) return null;
    if (!currentUser.data) return null;
    
    return currentUser.data;
}

function getUserData() {
    
}
``` 

Use `if return return`, instead of `if else`.

Bad:

```TypeScript
if (!isObjectExists && !isCacheExists) {
    return new ObjectDTO();
} else {
    return allObjects[key];
}
```

Good:

```TypeScript
if (!isObjectExists && !isCacheExists) {
    return new ObjectDTO();
}

return allObjects[key];
```

We follow the Law of `Demeter`, meaning that when accessing an object or service, we use only one dot notation, such as `someObject.someProperty`.

We adhere to the `DRY` principle, which means not repeating the same text, expression, or code. 
Instead, we use constants, enums, classes, and functions.

Regarding mutations:

1. We avoid making mutations inside components to avoid problems with the onPush strategy.
2. We avoid changing references to an object's properties from within nested functions. 
Instead, we try to do so where the object is initialized. If the object is a member of a class, it is preferable to change it within the called function.

Bad:

```TypeScript
const user = {....}
  f1() {
    f2() {
      f3() { user.property = true }
      }
    }
```

Good:

```TypeScript
let user = {....}
const property: boolean = f1();
user = {...user, aSomeFiled: property }

f1() { return f2(); }
f2() { return f3(); }
f3() { return true; }
```

For some rxjs operators we recommend to use `array destructuring`.

Bad:

```TypeScript
return zip(isYellow$, isGreen$).pipe(
    map((dataUser) => { if(dataUser[0]) {...} ...)})
)
```

Good:

```TypeScript
return zip(isYellow$$, isGreen$).pipe(
    map(([isYellow$, isOwner]) => { if(isYellow) {...} ...)})
)
```

## Angular

For all components by default we use the `OnPush` strategy. To do this by default, we specify this rule in `angular.json`.

```Json
"@schematics/angular:component": {
  "changeDetection": "OnPush"
}
```

Following the `single responsibility principle`, we split large components, services, and templates into smaller ones. 
This makes them easier to test and maintain. 
Our goal is to limit `HTML` templates to `75` lines of code; if they exceed this limit, we split them into smaller templates. 
Similarly, we aim to keep components, services, and directives under `400` lines of code. 
If they surpass this limit, we break them down into smaller pieces.

We only use `RxJS` and not `Promise` because:

1. Unsubscribing from `async/await` is problematic as it requires constant figuring out of the cancellation token. In RxJS, everything is already sorted out.
2. `Timeouts` and `retries` are easily managed with `RxJS`.
3. Requests can be started declaratively using pipe `| async`, without relying on lifecycle.
4. We can make requests dependent on certain states such as filtering, pagination, and search.

We don't inherit components from one another. We use services, directives, and parent class, among other things, to include shared code.

We apply the trackBy function to all `ngFor`.

We avoid cyclical dependencies of entities like services and classes. When they arise and A depends on B and vice versa, we put them together inside C (A, B) and use C.

We give the names `Input`, `Output` so that it is clear from the outside which one they were performing.

Bad:

```Html
<phone-edit-component
    [editMode]="isEdit"
    [canEdit]="true"
    [canDelete]="false"
    [canSave]="true"
    [canCancel]="true"
    [isDraftValid]="isDraftValid$ | async"
    (cancel)="onCancel()"
    (save)="onSave()"
></phone-edit-component>
```

Good:

```Html
<phone-edit-component
    [isShowEditAndDelete]="!isEdit"
    [isShowEdit]="true"
    [isShowDelete]="false"
    [isShowSave]="true"
    [isShowCancel]="true"
    [isSaveEnable]="isFormValid"
    (cancel)="onCancel()"
    (save)="onSave()"
    (edit)="onEdit(release)"
></phone-edit-component>
```

We recommend to follow this order of declarations for component controller properties:

```
viewchild / viewchildren
input
output
(property) public
(property) private
constructor()
get
set
(method) public
(method) private
```

When creating the objects, we use the `OOP` approach. Since `Angular` is designed for OOP.
Therefore, we do not create objects through a function, but we make classes.

Bad:

```TypeScript
public customer = (data?: CustomerModel): CustomerModelDTO => {
    const newData: CustomerModelDTO = {
        id: data?.id || this.FillCustomerService.id(),
        name: data?.name || this.FillCustomerService.title(),
        description: data?.description || this.FillCustomerService.description()
    };

    return newData;
};
```

Good:

```TypeScript
public customer = new Customer(data?: CustomerModel);
```

Avoid to use abbreviations in the names of the components, give the full name.

Bad:

`CurUsrCreateComponent`

Good:

`CurrentUserCreateComponent`

Start component names with nouns, not verbs.

Bad:

`CreateTableComponent`

Good:

`TableComponentCreate`

Start properties names with nouns or `is`, not verbs.

Bad:

```TypeScript
@Input() setAccountInfoModel: AccountInfoModel = {
    name: '',
    surname: ''
};
```

Good:

```TypeScript
@Input() accountInfoModel: AccountInfoModel = {
    name: '',
    surname: ''
};
```

We indicate explicitly `null` or` undefined` if the function's argument, function's result, class's property, etc.
Indicate what you expect or don't expect to receive, return `null` or`unefined (void)`.

Bad:

```TypeScript
@Input() shopName: string;
....
if (this.userName) {
....
}
```

Good:

```TypeScript
@Input() shopName: string | null = null;
....
if (this.userName) {
....
}
```

If you need to track changes to `@Input()`, for example, to execute some logic, you can use a `setter` for this purpose.
It is not recommended to use `OnChanges` for this, as it does not support type checking by default, which can result in errors following input refactoring.
But if you have got a `glitch` effect, then use `ngOnChanges`, and you should definitely wrap `SimpleChanges` in` Generic`.

Bad:

```TypeScript
ngOnChanges(changes: SimpleChanges): void {
    const marker = changes.CurrentObject?.nextValue;
    if(marker) {
        this.marker = marker;
    }
}
```

Good:

```TypeScript
@Input()
set marker(marker: string) {
    this.marker = marker;
}

public marker: string = '';
```

When you need to set a value after `@Input` changes, it's better to use a getter instead of `ngOnChanges`.

Bad:

```TypeScript
ngOnChanges(): void {
    this.isSectionEditable = this.section.status === this.sectionStatuses.InProgress && this.isAdministrator;
}
```

Good:

```TypeScript
public get isSectionEditable(): boolean {
    return this.isSectionEditable = this.section.status === this.sectionStatuses.InProgress && this.isAdministrator;
}
```

Let's give `Input` and `Output` meaningful names so that their purpose is clear from the outside.
This way we can quickly understand what kind of work they are performing.

When updating or initializing the data for any UI element, such as `mat-table`, it is important to keep in mind that this UI element is created after calling the `ngAfterViewInit` life cycle hook.
Here are the steps you should follow to update its data:

1. Store the data in the parent component controller, within `@Input()`, `subscription`, or other.
2. Check if the UI element exists and then update its data.
3. Set the UI element data inside `ngAfterViewInit`.

Bad:

```TypeScript
@Input() set roles(roles: UserRoles) {
    this._roles = roles;

    this.setParametersDataSource(this._roles.info);
}
```

Good:

```TypeScript
@Input() set roles(roles: UserRoles) {
    this._roles = roles;

    if (this.tableParametrs) {
        this.setParametersDataSource(this._roles.info);
    }
}

public ngAfterViewInit(): void {
    if(this._roles) {
        this.setParametersDataSource(this._roles.info);
    }   
}
```

Use only one returning type inside `guard` or `resolver`, avoid of using type union.

Bad:

```TypeScript
canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.permissions.canActivate(this.currentUser, route.params.id);
}
```

Good:

```TypeScript
canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
): Observable<boolean> {
    return this.permissions.canActivate(this.currentUser, route.params.id);
}
```

### Mocking

#### Mocking via service

To avoid being dependent on a third-party service, such as a back-end, and to receive any necessary data during development, we should simulate/mock data reception.
For instance, if a programmer wants to receive an error from the back-end and verify it on the UI, they can mock some http.service and generate an exception there.

To accomplish this, follow these steps:

1. Create a base class `abstract class DataService` and describe the required methods in it.
2. Create a service `class DataHttpService extends DataService`, inherit from the abstract, and write the intended implementation, for instance, by adding real requests to the back-end.
3. Create a service `class DataHttpMockService extends DataService` and define the necessary implementation. This file should be named using the mask `*****-mock.service.ts`.
4. Inject the abstract class `DataService` into the components or other services.
5. Create a provider called `DataServiceProvider` in a separate file. Write the contents of the provider like `export const DATA_SERVICE_PROVIDER: Provider = { provide: DataService, useValue: environment.isUseMock? new DataHttpMockService(): new DataHttpService()};`. Register the conditions and choose to use either the real or the mock implementation through the use of `useClass` or `useFactory`.
6. Insert the provider into the module providers: `[DATA_SERVICE_PROVIDER]`.
7. Store the provider in the `Providers` folder of this module.
8. Store the Mock in the same folder as the implementation.
9. Preferably, inject abstractions over real implementations because passing the implementation can violate the Dependency Inversion Principle.

#### Mocking via backend

Additionally, you can develop your own backend and send responses to your Angular application. 
In my opinion, this is a better approach than mocking data with services.

To implement this, follow these steps:

1. Create a `proxy.conf.json` file.
2. Add the path to the `proxy.conf.json` file in your `angular.json` file. You can find instructions on how to do this here: https://angular.io/guide/build#proxying-to-a-backend-server.
3. Start your backend server.

### Template

The order of attributes in the template should be as follows:

1. directives.
2. static attributes.
3. css classes.
4. inputs.
5. outputs.

Bad:

```Html
<table-component class="table-body" [data]="dataTable" *ngIf="user.isVisible" id="0" (data)=setDataTable($event)>
</table-component>
```

Good:

```Html
<table-component *ngIf="user.isVisible" id="0" class="table-body"  [data]="dataTable" (data)=setDataTable($event)>
</table-component>
```

We attempt to build our own model for each component as needed. 
This model must be mapped in the component above for both input and output. 
For instance, if the parent contains an object with 5 properties, but the child only needs 4 of those properties, the child should create a custom interface with those 4 fields and wait for them to be input. 
The parent should handle the conversion from 4 to 5 properties. 
(parent m1 to m2) m2 -> child, child m2 -> parent (parent m2 to m1)

## RxJs

Always unsubscribe from the `subscription`.

Bad:

```TypeScript
this.userManagementService.setActivity(user, isActive)
    .subscribe();
```

Good:

```TypeScript
this.userManagementService.setActivity(user, isActive)
    .pipe(takeUntil(this.destroy$))
    .subscribe();
```

We use `takeUntil()`, `Subject<void>()`, `ReplaySubject<void>(1)` or `UnsubscribeService` for unsubscribe.

Example:

```TypeScript
private destroy$: new Subject<void>();

.pipe(
    switchMap(....),
    takeUntil(this.destroy$)
)
.subscribe();

ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
}
```

To unsubscribe, the operator `takeUntil(....)` should be placed before subscribe(). 
It should not be placed above.

Bad:

```TypeScript
this.route.queryParams.pipe(
    takeUntil(this.destroy$),
    switchMap(....)
).subscribe();
```

Good:

```TypeScript
this.route.queryParams.pipe(
    switchMap(....),
    takeUntil(this.destroy$)
).subscribe();
```

Do not unsubscribe in the `takeUntil(...)` method if there was no call to `subscribe()`.

Bad:

```TypeScript
this.filteredCities$ = this.api.getFilteredCities().pipe(
    switchMap(....),
    takeUntil(this.destroy$)
);
```

Good:

```TypeScript
this.filteredCities$ = this.api.getFilteredCities().pipe(
    switchMap(....)
);
```

We don't make subscriptions in the constructor, instead we make them in `OnInit` or `AfterViewInit` depending on the situation.

Bad:

```TypeScript
constructor(private publicationService: PublicationsService) {
    this.publicationService.getPublications(this.searchModel)
        .pipe(takeUntil(this.destroy$))
        .subscribe(publications => {
            this.publications = publications;
        });
}
```

Good:

```TypeScript
ngOnInit() {
    this.publicationService.getPublications(this.searchModel)
        .pipe(takeUntil(this.destroy$))
        .subscribe(publications => {
            this.publications = publications;
        });
}
```

Do not call `.subscribe(() => ...)` within another subscription, such as `.subscribe(() => subscribe())`.
If you need to retrieve data from another stream within a subscription, you should use `switchMap` or other `rxjs` higher-order operators.

Bad:

```TypeScript
this.matchingService.getMatchings().pipe(takeUntil(this.destroy$))
    // First subscription
    .subscribe(matchings => {
        matchings.matchingShared$
            .pipe(takeUntil(this.destroy$))
            // Second subscription
            .subscribe(matchingShared => {
                this.matchingShared = matchingShared;
            });
    });
```

Good:

```TypeScript
this.matchingService.getMatchings().pipe(
    switchMap(matchings => matchings.matchingShared$),
    takeUntil(this.destroy$)
)
    // Single subscription
    .subscribe(matchingShared => {
        this.matchingShared = matchingShared;
    });
```

To avoid any `side effects`, we make an effort not to call `subject.next()` within the stream. 
Instead, we use a `pipe()` to generate the common portion of the stream and use it. 
Then, we can expand on the general part of the operator.

Bad:

```TypeScript
someDataSource$.pipe(....
    tap(data=> this.someSubject.next(data))
);
```

Good:

```TypeScript
const oneStream$ = someDataSource$.pipe(....);
const otherStream$ = someDataSource$.pipe(....);
```

Inside RxJS streams, you should only make assignments or call other methods in the `tap` operator or `subscribe` method.

For components, if you are not using the `| async` pipe, it is preferable to make assignments or call other methods (side effects) in the `subscribe` method instead of `tap`.
Otherwise, use `tap`.

Bad:

```TypeScript
this.cityService.state$.pipe(
    tap(allStates => {
        if (!allStates) {
            this.getDraftBlank();
        }
    }),
    takeUntil(this.destroy$))
    .subscribe()
```

Good:

```TypeScript
this.cityService.state$.pipe(
    filter(allStates => !allStates),
    takeUntil(this.destroy$))
    .subscribe(() => this.getDraftBlank()
    )
```

If the method needs to receive data from another stream, there's no need to create a subscription within the method.
Here's what you should do instead:

1. Create a `subject`.
2. Subscribe to it in any Angular lifecycle method such as `ngOnInit`, and pass the required stream within its `pipe()` method, using higher-order operators like `switchMap`.
3. Call `subject.next()` within the method.
4. Retrieve the data in `subscribe()` or through `| async`.

Example:

```TypeScript
// Subject
const subject: Subject<number> = new Subject();
// Main stream
const data$: Observable<number> = of(20);

// Make a subscription
ngOnInit {
    subject.pipe(switchMap(_ => data$), takeUntil(this.destroy$))
        .subscribe(....);
}

// Emit the subject inside method
public onButtonClick(): void {
    this.subject.next();
}
```

Instead of using `setTimeout()`, it is better to use `Subject.subscribe()` and the RxJS operator `delay`.

Bad:

```TypeScript
getNextPoint(): void {
    setTimeout(() => {
        this.initPoints();
    }, 100);
}
```

Good:

```TypeScript
ngOnInit {
    this.subject.pipe(delay(100), takeUntil(this.destroy$)).subscribe(() => this.initPoints());
}

public getNextPoint(): void {
    this.subject.next();
}
```
We do not subscribe to methods that are often called by the framework, such as `ngOnChanges`.

Bad:

```TypeScript
ngOnChanges(changes: SimpleChanges): void {
    this.customerEditService
        .getTemplate()
        .pipe(takeUntil(this.unsubscribeService$))
        .subscribe();
```


Do not pass (`Observable`, `Subject`, etc.) as arguments to the function, neither retrieve the result of these functions.
Instead, call them through chaining, using the `pipe` operators.

Bad:

```TypeScript
public getTransformModel(transfrormText: string): Observable < TransformModel > {
    ....
    const items: Observable<ItemType[]> = this.itemsWhereMoreOne(allItems);

const TransformModel: Observable<TransformModel> = this._getTransformModelFromItemTypes(items);

return TransformModel;
};


private itemsWhereMoreOne(allItems: Observable < ItemTypeWithTotalCount > []):
Observable < ItemType[] > {
    return forkJoin(allItems).pipe(
        map((itemsTypeWithCount: ItemTypeWithTotalCount[]) => ....)
    );
}

private _getTransformModelFromItemTypes(items: Observable<ItemType[]>):
Observable < TransformModel > {
    return items.pipe(
        map((itemsTypesWithCount: ItemType[]) => {....})
    );
}
```

Good:

```TypeScript
public getTransformModel(transfrormText: string): Observable < TransformModel > {
    ....
    return this.itemsWhereMoreOne(allItems).pipe(
        switchMap((items) => this._getTransformModelFromItemTypes(items))
    );
};

private itemsWhereMoreOne(allItems: Observable < ItemTypeWithTotalCount > []):
Observable < ItemType[] > {
    return forkJoin(allItems).pipe(
        map((itemsTypeWithCount: ItemTypeWithTotalCount[]) => ....)
    );
}

private _getTransformModelFromItemTypes(items: Observable<ItemType[]>):
Observable < TransformModel > {
    return items.pipe(
        map((itemsTypesWithCount: ItemType[]) => {....})
    );
}
```

## Architecture

Components used next logic:

1. Use Input/Output.
2. Display data.
3. Communicate with services, such as facade, etc.
4. Contain business logic.
5. Are responsible for routing.
6. Contain logic for using nested components.

To transfer data between parent and child components, we use the input and output.
To send an event or data from parent to child, follow these steps:

1. Create an `abstract class`, such as TransferSomeData, and define its methods.
2. Extend the child component from the `abstract class` and implement its methods.
3. Inside the parent component, use `@ViewChild('id') transferData: TransferSomeData` and call the abstract class methods.

Entities can be sorted into three types:

1. VM - contains data needed for the component.
2. Model - describes business logic. It is used as a value object and should not include methods.
3. DTO - contains data sent or received from the backend.

Following the Dependency Inversion Principle, you should avoid directly injecting your Redux store, HTTP services, and components into each other. 
Therefore, I suggest creating abstractions for each of them. Specifically:

1. Abstraction for state managers.
2. Abstraction for any HTTP or WebSockets services.
3. Abstraction for any local or session storage services.
4. Abstraction for any web worker services.

Instead of using `providedIn: 'root'`, it's better to provide services inside core or app modules. 
This approach makes it clear to locate where the services are being provided.

Bad:

```TypeScript
@Injectable({
    providedIn: 'root'
})
export class CustomerSettingService {}
```

Good:

```TypeScript
@Injectable()
export class SearchTagsService{}

@NgModule({
    providers: [
        SearchTagsService
        ]
})
export class SharedModule {}
```

## Comments

These comments help make the code more readable and reduce the chance of errors or confusion. 
It's always a good idea to use clear and concise comments when writing Angular code.

Bad:

```TypeScript
// This is a component
@Component({
    selector: 'app-example',
    templateUrl: './example.component.html',
    styleUrls: ['./example.component.css']
})
export class ExampleComponent {
    constructor() {
        // This is where we do stuff
    }

    // This is a function
    doStuff() {
        // This is where we do more stuff
        return true;
    }
}
```

Good:

```TypeScript
/**
 * This component is responsible for displaying an example.
 * It receives input data from a parent component and handles user interactions.
 */
@Component({
    selector: 'app-example',
    templateUrl: './example.component.html',
    styleUrls: ['./example.component.css']
})
export class ExampleComponent {
    
    constructor() {
    }

    /**
     * Performs an action when the user clicks a button.
     * @param event - the user's click event
     */
    handleUserClick(event: MouseEvent) {
    }
}
```

## CSS styles

To specify component styles in the component styles file, such as `*.component.css` or `*.component.scss`, you should use the appropriate syntax.

If you need to set up a style for a component's `"root"` tag, for instance, components inside `<router-outlet>`,
you may employ the pseudo-class `:host` within your `*.component.css` or `*.component.scss`.

The :host selector only targets the host element of a component.
Any styles within the `:host` block of a child component will not affect parent components.

Example:

```css
:host {
  font-style: italic;
}
```

By following these best practices, you can efficiently use SVG in Angular, 
create reusable components and reduce overall load time of your application:

1. Use inline SVG: Inline SVG is a recommended way when working with Angular because it allows for greater flexibility and performance. You can use DOM accessors to manipulate SVG elements.
2. Use SVG as components: If you want to reuse SVG elements across the application, create SVG components. SVG components should always start with an SVG root element tag and should export a svg component.
3. Define default dimensions: Defining default dimensions for components help you in avoiding `FOUC` (Flash of unstyled content) syndrome, especially when SVG graphics are loaded asynchronously.
4. Optimize SVG files: SVG files can contain a lot of unnecessary code that increases their size. Before adding an SVG to an Angular project, optimize SVG files using tools such as SVGOMG or SVGO.
5. Keep viewBox attribute: viewBox attribute is an essential element when it comes to responsive SVGs. It’s recommended to set the viewBox attribute on SVGs so that they scale properly in different sizes.
6. Use `ng-attr-` directives: To support dynamic values and expressions use `ng-attr-` namespace, such as `ng-attr-fill="{{color}}"` instead of `fill="{{color}}"`.
7. Use Angular animations to animate SVGs: Finally, you can use Angular animations to create animations for SVG elements in the application.

## State manager

Managing state in an Angular application is tricky. 
It’s important to understand the best practices for managing state, so that your application remains easy to use and maintain.

If you need to store fewer than 10 business models, you may use services. 
However, if you have more than 10, I recommend using a state manager. 
This is especially useful for beginner front-end engineers. 
It doesn't matter which specific state manager you choose. 
If you need to track the status of an object stored within, you can wrap it in a generic.

Make all interactions with the store using an abstract class, following the `Dependency Inversion Principle`.

1. We should provide the facade at the module level.
2. Inject the abstract class into the component.
3. The resulting component or service with business logic should not have any imports from the state manager library.

Abstract names should be given to the facade's methods and should not repeat the names of the current state manager. 
For example, you can use verbs such as `create, read, update, delete, set, change, update, load`, followed by the business model.

Bad:

```TypeScript
// The state service
export class StateService {
  private _state: object = {};
  getState() {
    return this._state;
  }
  setState(data: object) {
    this._state = data;
  }
}

// The component class
export class MyComponent implements OnInit, OnDestroy {
    constructor(private _stateService: StateService) {
    }

    ngOnInit(): void {
        const state = this._stateService.getState();
    }
}
```

Good:

```TypeScript
import { Subject } from 'rxjs';
// The button click observable
const buttonClick$ = new Subject();
// The state service
export class StateService {
    private _state: object = {};
    getState() {
        return this._state;
    }
    setState(data: object) {
        this._state = data;
    }
    // Subscribe to the button click observable
    subscribeToButtonClick(cb: Function) {
        buttonClick$.subscribe(cb);
    }
}

// The component class
export class MyComponent implements OnInit, OnDestroy {
    constructor(private _stateService: StateService) {
    }

    ngOnInit(): void {
        this._stateService.subscribeToButtonClick((data) => {
            this._stateService.setState({ ...this._stateService.getState(), ...data });
        });
    }
}
```

### NGXS

We keep different entities in separate folders, like:
- `"actions"` folder contains various action classes.
- `"models"` folder stores interfaces, classes, and their predefined values.
- `"states"` folder holds action effects and selectors.

We define our state model interface that defines the shape of our state. 
We then use the `@Selector()` decorator to define our selectors and make use of the state model to strongly type the input parameters and return values of our selectors.
By passing in our state model to the `@Selector()` decorator, we ensure that our selectors are only called when the relevant part of the state changes. 
This makes our selectors efficient and dependable.

Bad:

```TypeScript
export class CounterState {
    @Selector()
    static count(state: number) {
        return state;
    }

    @Selector()
    static evenCount(state: number) {
        return state % 2 === 0;
    }
}
```

Good:

```TypeScript
export interface CounterStateModel {
    count: number;
}

export class CounterState {
    @Selector([CounterState])
    static getCount(state: CounterStateModel) {
        return state.count;
    }

    @Selector([CounterState])
    static isCountEven(state: CounterStateModel) {
        return state.count % 2 === 0;
    }
}
```

The beginning of function names in the `actions` folder will be from a verb.

Bad:

```TypeScript
NewContacts
```

Good:

```TypeScript
CreateNewContacts
DeleteHistory
```

Give a names to action class as action type name.

Bad:

```TypeScript
export class GetHeader{
    public static readonly type: string = PublicationCreateActions.GetHeaderStatus;
}
```

Good:

```TypeScript
export class GetHeaderStatus {
    public static readonly type: string = PublicationCreateActions.GetHeaderStatus;
}
```

When using the `Redux dev tools`, the moment objects may appear as a string. 
Therefore, it is important to exercise caution when inspecting the state.
It should be noted that `ngxs` operates outside of `ngZone` by default. 
If you need to display dialogs or other UI components from an `action`, you must explicitly do it within `ngZone`.

Bad:

```TypeScript
import { Component, OnInit, NgZone } from '@angular/core';
import { Store } from '@ngxs/store';
import { MyState } from './mystate.state';

@Component({
    selector: 'app-my-component',
    template: `
    <div>{{myState$ | async}}</div>
  `,
})
export class MyComponent implements OnInit {
    myState$: Observable<MyState>;

    constructor(private store: Store, private ngZone: NgZone) { }

    ngOnInit() {
        this.myState$ = this.store.select(MyState);

        // dispatch an action outside of ngZone
        this.store.dispatch(new MyAction());

        // listen to state changes outside of ngZone too
        this.myState$.subscribe((state) => {
            // do stuff inside ngZone
        });
    }
}
```

Good:

```TypeScript
import { Component, OnInit, NgZone } from '@angular/core';
import { Store } from '@ngxs/store';
import { MyState } from './mystate.state';

@Component({
    selector: 'app-my-component',
    template: `
    <div>{{myState$ | async}}</div>
  `,
})
export class MyComponent implements OnInit {
    myState$: Observable<MyState>;

    constructor(private store: Store, private ngZone: NgZone) { }

    ngOnInit() {
        this.myState$ = this.store.select(MyState);

        // dispatch an action inside ngZone
        this.ngZone.run(() => {
            this.store.dispatch(new MyAction());
        });

        // listen to state changes inside ngZone too
        this.myState$.subscribe((state) => {
            this.ngZone.run(() => {
                // do stuff inside ngZone
            });
        });
    }
}
```

## Forms

If you don't want to modify the entire structure within the forms, 
you can create a custom interface known as `Vm****`.
You can either create one yourself or choose one from another source.

Example:

```TypeScript
export interface RegisterFormModel {
    name: string;
    email: string;
    password: string;
    phone: string;
    age: number;
}

export interface VmCredentials extends Pick<RegisterFormModel, 'email' | 'password'> {
    email: string;
    password: string;
}

@Injectable()
export class UserService {
    constructor() { }

    findUser({ email, password }: VmCredentials): UserModel {....}
}
```

### Formly

We use a setter that includes a mandatory check for `null` and cloning when setting the model,
as Formly has the ability to modify the object during runtime. 
Without this check, an error can occur when retrieving the model from the `store` since the store often freezes its models.

Example:

```TypeScript
@Input() set model(model: VmCredentials) {
    if (model) {
        this._model = { ...model };
    }
}
```

Make sure to declare and initialize the default model. 
Otherwise, the form will generate a model with only one property the first time, which may cause errors.

Ensure that you set the shape key values from the model using the specific function, 
such as `public static getFieldName = <T> (name: keyof T) => name;`. 
We do this to accurately synchronize the key-model pairing. 
If not, Formly may add unclear properties to the model, which will be sent to the store.

Example:

```TypeScript
this.fields = [
    {
        key: Utils.getFieldName<VmCredentials>('email'),
        ....
    ]
}
```

To ensure optimal functionality, we emit a `model` update when there is a change made by the user themselves or in the options. 
This is because `ValueChange` is triggered frequently and may create problems. 
In our ideal scenario, the `model` will only be updated when the user manually modifies something. 
To achieve this, it is recommended to clone the model first using a `spread` operator or specific libraries such as `lodash`.

Example:

```TypeScript
public onModelChange(model: SomeFormModel) {
    this.someFormModelChange.emit({ data: { ...model } });
}
```

When you make changes to the form options, the `model` will be automatically updated.
In order to prevent this, we have implemented the following solution:

Example:

```TypeScript
import { OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

export class MatchingCommentDialogComponent implements OnInit {
    @Input() set isFormEnable(isEdit: boolean) {
        this._isFormEnable = isEdit;
        this.changeFormEnable(isEdit);
    }

    public formGroup: FormGroup;
    public options: FormControl;

    private _isFormEnable: boolean;

    private changeFormEnable(isEnable: boolean): void {
        this.options.formState.disabled = !isEnable;
        const disableOptions = {
            onlySelf: true,
            emitEvent: false,
        };

        if (isEnable) {
            this.formGroup.enable(disableOptions);
            return;
        }
        this.formGroup.disable(disableOptions);
    }
}
```