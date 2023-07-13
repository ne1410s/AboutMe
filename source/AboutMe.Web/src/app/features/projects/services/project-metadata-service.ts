import { Injectable } from '@angular/core';
import { of } from 'rxjs';

import { ProjectMetadata } from '../models/project-metadata.interface';

@Injectable({ providedIn: 'root' })
export class ProjectMetadataService {

  private readonly projects: ProjectMetadata[] = [
    { title: 'acme', link: 'acme', description: '' }
  ];

  getProjects() {
    return of(this.projects);
  }
}
