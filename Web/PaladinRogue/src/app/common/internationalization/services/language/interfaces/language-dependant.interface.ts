export interface ILanguageDependant {
  setLanguage(languageId: string): Promise<void>;
}
