import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { ProjectMetadata } from '../models/project-metadata.interface';

@Injectable({ providedIn: 'root' })
export class ProjectMetadataService {

  private readonly projects: ProjectMetadata[] = [
    new ProjectMetadata('ACME', 'acme', 'Free SSL certificates with Let\'s Encrypt'),
    new ProjectMetadata('Comanche', 'comanche', 'Swagger for the command line'),
    new ProjectMetadata('Crypto Stream', 'crypto-stream', '', false),
    new ProjectMetadata('Custom Elements', 'custom-elements', '', false),
    new ProjectMetadata('Fluent Errors', 'fluent-errors', 'Semantic response codes'),
    new ProjectMetadata('Griddler', 'griddler', 'Fun puzzle'),
    new ProjectMetadata('Libra', 'libra', '', false),
    new ProjectMetadata('PSR', 'psr', '', false),
  ];

  getProjects(): Observable<ProjectMetadata[]> {
    return of(this.projects);
  }

  isEnabled(link: string): boolean {
    return this.projects.find(p => p.link === link)?.enabled || false;
  }
}
