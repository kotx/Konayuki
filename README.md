# Konayuki

[![Nuget](https://img.shields.io/nuget/vpre/Konayuki.svg)](https://nuget.org/packages/Konayuki)

A (hopefully) fast and efficient ID generator.

Konayuki spits out 64-bit ulong IDs, because an unused sign bit is a waste.

## Structure

A default Konayuki ID is structured like this:

| Timestamp   | ID          | Sequence    |
| ----------- | ----------- | ----------- |
| 42 bits     | 8 bits      | 14 bits     |

However the number of bits per field is customizable, as long as all three add up to 64 bits!

## Samples

The following samples are located at [samples](/samples):

| Link                                                      | Description                                                |
| --------------------------------------------------------- | ---------------------------------------------------------- |
| [01_basic_id_generation](/samples/01_basic_id_generation) | Basic ID generation example. Throws on sequence overflows. |
| [02_custom_epoch](/samples/02_custom_epoch)               | An example of using a custom epoch value.                  |
| [03_overflow_handling](/samples/03_overflow_handling)     | Showcases different sequence overflow strategies.          |

## Documentation

The latest documentation is available at [konayuki.yukata.tech](https://konayuki.yukata.tech) or [kotx.github.io/konayuki](https://kotx.github.io/Konayuki).

## Customization

You may implement `IKonayuki` for a blank slate or `Konayuki` for customizing the generator.

If you want to use a custom epoch, derive from `KonayukiOptions`.