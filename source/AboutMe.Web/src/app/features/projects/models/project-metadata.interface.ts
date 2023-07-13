export class ProjectMetadata {
  constructor(
    public title: string,
    public link: string,
    public description: string,
    public enabled: boolean = true,
  ) {}
}