import { HttpMethod } from '../../constants/http-method/http-method.constant';

export interface ILinkData {
  href: string;
  allowVerbs: Array<HttpMethod>;
}
