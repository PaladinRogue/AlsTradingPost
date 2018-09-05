declare global {
  interface String {
    format(...params: Array<any>): string;

    splitIntoLengthParts(length: number, reverse: boolean): Array<string>;
  }
}

String.prototype.format = function (...params: Array<any>): string {
  let result: string = this;
  params.forEach((param: any, index: number) => {
    const regex: RegExp = new RegExp(`\\{${index}\\}`, 'g');

    result = result.replace(regex, param);
  });

  return result;
};

String.prototype.splitIntoLengthParts = function (length: number, reverse: boolean): Array<string> {
  const result: Array<string> = [];
  let accumulator: string = this;
  if (reverse && accumulator.length % length !== 0) {
    result.push(accumulator.substr(0, accumulator.length % length));
    accumulator = accumulator.substr(accumulator.length % length);
  }

  for (let i: number = 0; i < accumulator.length; i += length) {
    result.push(accumulator.substr(i, length));
  }
  return result;
};

export {};
