# Changelog

All notable changes to **ValueStringBuilder** will be documented in this file. The project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

<!-- The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) -->

## [Unreleased]

### Added

- Support for `net7.0` and `net8.0`

## [1.3.0] - 2023-05-07

### Changed

-   `Sum` uses a better implementation that reduces 33% of the time needed to calculate. This has direct performance improvements on `Average` as well.
-   `Min`, `Max` also have a better runtime (also 33% faster) thanks to a better algorithm.

## [1.2.2] - 2023-04-12

### Added

-   Smaller improvement in `Sum`

## [1.2.1] - 2023-04-10

## [1.2.0] - 2023-04-04

### Added

-   New `Contains` method to check if an collection contains a specific value.

## [1.1.0] - 2023-04-04

### Added

-   Overloads for all functions that expects a `Memory<T>` object.

## [1.0.0] - 2023-04-01

This is the initial release!

### Added

-   Added `Min`, `Max`, `Sum`, `SequenceEqual` and `Average`

[Unreleased]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.3.0...HEAD

[1.3.0]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.2.2...1.3.0

[1.2.2]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.2.1...1.2.2

[1.2.1]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.2.0...1.2.1

[1.2.0]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.1.0...1.2.0

[1.1.0]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/1.0.0...1.1.0

[1.0.0]: https://github.com/linkdotnet/LinqSIMDExtensions/compare/e70becb4068b55fd771e09975d6b223076ce6d2c...1.0.0
