# FsFhtw - se22m060

## Business Problem

The program should be able to create Orders containing Products and Categories in order to be able to categorize said products.
All Orders are a part of a particular Store.

## Domain

The Domain consists of the following types:

- Type `Cat` which has a property `Name` of type string.
- Type `Customer` which has a property `Name` of type string.
- Type `Product` which has a property `Name` of type string.
- Type `Order` which has a property `Products` which is a list of type `Product`, a property `Categories` which is an array of type `Cat` and a property `Customer` which is of type `Customer`
- Type `Store` which has a property Orders which is a list of type Order

## Usage

### Adding a new Order and Undo/Redo

- 'AddOrder' [`products`] 'Category' [`categories`]
- 'AddOrder' [`products`] 'Category' [`categories`]

`products` and `categories` are strings separated by a blank space

In the following example an order consisting of `3 products` and `2 categories` is placed:
> Example: AddOrder `Flour Eggs Milk` Category `Baking Ingredients`

The last order can also be undone:
> UndoOrder

The previously undone order can also be redone:
> RedoOrder

## Challenges

- Handling UserInput with Active Patterns
- Handle Immutability for the State (Push/Undo/Redo) -> still unsure if correct but it seems to work :)
- Modelling nested Types
