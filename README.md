# Potter Kata
## Overview
I like to do TDD Kata's occasionally in my free time. The Potter Kata is a deceivingly challenging one & is often simplified to suffice at returning £51.60 for the final scenario because it quickly becomes more of a complex problem solving/optimisation algorithm exercise rather than a Kata designed to sharpen ones ability to TDD.

Regardless, here is a *(far from perfect)* solution that I have come up with in order to both refresh myself with C# alongside generate discussion with colleagues at the Coding Dojo I run at work. It further caters to the below problem description by also being generic enough to cater for bespoke Discount Service's for new book collection types with ease. These can be seen in the implementation done for Lord of the Rings books & O'Reilly books. This solution could still be improved through some trivial tweaks such as representing the basket as a collection of Book objects rather than merely an array of integers, renaming the project to something more generic, etc.

As part of my red-green-refactor loops, I also refactor multiple tests to become singular tests that utilise theories/parameterised tests in xUnit. This makes it slightly more difficult to track the order within which the Programmer/Developer Tests (what Kent Beck means when he says Unit Tests in his book on Test Driven Development) were written. However in this case, all tests within `GivenBookSetDiscountCalculator` were written in the same sequential order.

Improvements submitted as PR's are more than welcome.

## Problem Description
Once upon a time there was a series of 5 books about a very English hero called Harry. (At least when this Kata was invented, there were only 5. Since then they have multiplied) Children all over the world thought he was fantastic, and, of course, so did the publisher. So in a gesture of immense generosity to mankind, (and to increase sales) they set up the following pricing model to take advantage of Harry’s magical powers.

One copy of any of the five books costs £8. If, however, you buy two different books from the series, you get a 5% discount on those two books. If you buy 3 different books, you get a 10% discount. With 4 different books, you get a 20% discount. If you go the whole hog, and buy all 5, you get a huge 25% discount.

Note that if you buy, say, four books, of which 3 are different titles, you get a 10% discount on the 3 that form part of a set, but the fourth book still costs £8.

Potter mania is sweeping the country and parents of teenagers everywhere are queueing up with shopping baskets overflowing with Potter books. Your mission is to write a piece of code to calculate the price of any conceivable shopping basket, giving as big a discount as possible.

For example, how much does this basket of books cost?

- 2 copies of the first book
- 2 copies of the second book
- 2 copies of the third book
- 1 copy of the fourth book
- 1 copy of the fifth book

```
  (4 * 8) - 20% [first book, second book, third book, fourth book]
+ (4 * 8) - 20% [first book, second book, third book, fifth book]
= 25.6 * 2
= £51.20
```
Source: https://codingdojo.org/kata/Potter/