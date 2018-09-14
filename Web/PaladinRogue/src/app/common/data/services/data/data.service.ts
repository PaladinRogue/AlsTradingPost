import { ICollectionResource } from '../collection-resource/collection-resource.interface';
import { ILink } from '../link/link.interface';
import { IResource } from '../resource/resource.interface';

export abstract class DataService {
  public abstract create(link: ILink, resource: IResource): Promise<IResource>;

  public abstract get(link: ILink): Promise<IResource>;

  public abstract getAll(link: ILink): Promise<ICollectionResource>;

  public abstract change(link: ILink, resource: IResource): Promise<IResource>;

  public abstract delete(link: ILink): Promise<void>;
}
