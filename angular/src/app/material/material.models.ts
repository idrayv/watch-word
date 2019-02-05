export enum MaterialMode {
  Read, Edit, Add
}

export class MaterialStats {
  public name: string;
  public value: string;
}

export class VocabWordFiltration {
  public showLearnWords: boolean;
  public showKnownWords: boolean;
  public showUnsignedWords: boolean;

  constructor(showLearnWords = true, showKnownWords = false, showUnsignedWords = true) {
    this.showLearnWords = showLearnWords;
    this.showKnownWords = showKnownWords;
    this.showUnsignedWords = showUnsignedWords;
  }

  public clone(): VocabWordFiltration {
    return new VocabWordFiltration(this.showLearnWords, this.showKnownWords, this.showUnsignedWords);
  }
}
